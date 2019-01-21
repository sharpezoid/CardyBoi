using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    //GameController gc;
    public GameObject target;
    public GameObject line;

    public Hero owner;
    
    //public GameObject cardPrefab;
    public List<CardSceneObj> cards = new List<CardSceneObj>(); // List that holds all my ten cards
    public Transform startTransform;  //Location where to start adding my cards
    public Transform handTransform; //The hand panel reference
    public Transform discardTransform;
    public Transform exhaustTransform;
    
    //int howManyAdded; // How many cards I added so far
    float gapSize; //the gap I need between each card
    float handWidth; // how wide is a hand 
    float drawSpeed = 10;
    public const int MAX_HAND_SIZE = 10;

    public CardSceneObj hoveredCard = null;
    int hoveredCardIndex = -1;

    float totalTwist = 20f;
    // 20f for example, try various values
    //int numberOfCards = ...get this from your List or array
    float twistPerCard;// = totalTwist / numberOfCards;
    float startTwist;// = -1f * (totalTwist / 2f);

    int prevHoverIndex = -1;

    bool goneBeyondThreshold = false; // -- has a draggeed card gone far enough to be consideered in play
    const int CARD_DRAG_THRESHOLD = 100;

    // -- UI
    public Text drawDeckSizeText;
    public Text exhausePileSizeText;
    public Text discardPileSizeText;
    

    void Start()
    {
       // gc = FindObjectOfType<GameController>();
        //howManyAdded = 0;
        //gapFromOneItemToTheNextOne = 1.0f;
        handWidth = handTransform.GetComponent<RectTransform>().rect.width - GameController.instance.cardPrefab.GetComponent<RectTransform>().rect.width;

        UpdateDeckSizes();
    }

    public void UpdateDeckSizes()
    {
        discardPileSizeText.text = owner.discardPile.Count.ToString();
        exhausePileSizeText.text = owner.exhaustPile.Count.ToString();
        drawDeckSizeText.text = owner.drawDeck.Count.ToString();
    }

    private void Update()
    {
        UpdateCards();
    }

    public void SetHoveredCard(CardSceneObj card)
    {
        if (card == null || card.isDragged)
        {
            hoveredCard = null;
            hoveredCardIndex = -1;
        }
        else
        {
            hoveredCardIndex = cards.IndexOf(card);
            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.SetSiblingIndex(i);
            }
            hoveredCard = card;
            prevHoverIndex = hoveredCardIndex;
            cards[prevHoverIndex].gameObject.transform.SetAsLastSibling();
        }
    }

    public void StartDrag(PointerEventData eventData, CardSceneObj card)
    {
        goneBeyondThreshold = false;
    }

    public void UpdateDrag(PointerEventData eventData, CardSceneObj card)
    {
        // show target at mouse cursor.
        target.GetComponent<RectTransform>().position = eventData.position;

        // keep card locked in highlighted position

        // -- what are we over? show color change.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                // hit player
                target.GetComponent<Image>().color = Color.red;
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                // hit player
                target.GetComponent<Image>().color = Color.green;
            }
            else
            {
                // hit nowt
                target.GetComponent<Image>().color = Color.gray;
            }
        }

        // -- if we go below y threshold cancel the drag
        if (eventData.position.y < CARD_DRAG_THRESHOLD && goneBeyondThreshold)
        {
            EndDrag(eventData, card);
        }
         
        // -- show dotted curve between card and cursor

        // -- check beyond threshold
        if (eventData.position.y > CARD_DRAG_THRESHOLD)
        {
            goneBeyondThreshold = true;
        }
    }



    public void EndDrag(PointerEventData eventData, CardSceneObj card)
    {
        // -- if somehow we have managed to play card twice (OnDrop & OnDragEnd at once etc...)
        //if (card.isWaitingToPlay)
        //{
        //    return;
        //}

        // -- what are we over?
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (CanPlayCard(card))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    // hit player
                    TurnController.instance.AddCardToPlayedQueue(card);
                    owner.actions -= card.card.ActionCost;
                    owner.mana -= card.card.ManaCost;
                    card.isWaitingToPlay = true;
                    cards.Remove(card);

                    Vector3 targetPos = Vector3.zero;
                    RectTransform rt = card.GetComponent<RectTransform>();
                    targetPos.z = 10000;
                    rt.position = targetPos;

                    // -- Send to proper pile - done at end of sequence not at start
                    if (card.card.exhaust)
                    {
                        // -- send to exhaust pile
                        AddCardToExhaustPile(card);
                    }
                    else
                    {
                        // -- send to discard pile
                        AddCardToDiscardPile(card);
                    }

                    OnScreenDebug.instance.Log(card.card.Name);
                }
                else if (hit.transform.gameObject.tag == "Enemy")
                {
                    TurnController.instance.AddCardToPlayedQueue(card);
                    // hit enemy
                    owner.actions -= card.card.ActionCost;
                    owner.mana -= card.card.ManaCost;
                    card.isWaitingToPlay = true;

                    cards.Remove(card);

                    // -- Send to proper pile - done at end of sequence not at start
                    if (card.card.exhaust)
                    {
                        // -- send to exhaust pile
                        AddCardToExhaustPile(card);
                    }
                    else
                    {
                        // -- send to discard pile
                        AddCardToDiscardPile(card);
                    }

                    OnScreenDebug.instance.Log(card.card.Name);
                }
                else
                {

                    // hit nowt
                    if (card.card.TargetFlags == Card.Targets.None)
                    {
                        TurnController.instance.AddCardToPlayedQueue(card);
                        //card.PlayCard();
                        owner.actions -= card.card.ActionCost;
                        owner.mana -= card.card.ManaCost;
                        card.isWaitingToPlay = true;

                        cards.Remove(card);

                        Vector3 targetPos = Vector3.zero;
                        RectTransform rt = card.GetComponent<RectTransform>();
                        targetPos.z = 10000;
                        rt.position = targetPos;

                        // -- Send to proper pile - done at end of sequence not at start
                        if (card.card.exhaust)
                        {
                            // -- send to exhaust pile
                            AddCardToExhaustPile(card);
                        }
                        else
                        {
                            // -- send to discard pile
                            AddCardToDiscardPile(card);
                        }

                        OnScreenDebug.instance.Log(card.card.Name);
                    }
                }
            }
        }

        card.isDragged = false;

       // owner.statUI.SetStats(owner);

        UpdateDeckSizes();
    }

    bool CanPlayCard(CardSceneObj c)
    {
        bool canPlay = (owner.actions >= c.card.ActionCost &&
            owner.mana >= c.card.ManaCost || !c.isWaitingToPlay);

        return canPlay;
    }


    public void AddCard(CardSceneObj c)
    {
        RectTransform rt = c.GetComponent<RectTransform>();
        rt.position = startTransform.position;
        //cardSceneObj.card = c;
        cards.Add(c);

        c.isWaitingToPlay = false;

        //gapSize = handWidth / cards.Count;

        //twistPerCard = totalTwist / cards.Count;
        //startTwist = -1f * (totalTwist / 2f);

        c.SetHand(this);

        //statUI.SetStats();
    }

    public void DiscardHand()
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Debug.Log("Discarding CARD : " + cards[i].card.Name + " i: " + i);
            AddCardToDiscardPile(cards[i]);
        }
    }

    void UpdateCards()
    {
        gapSize = handWidth / cards.Count;

        twistPerCard = totalTwist / cards.Count;
        startTwist = -1f * (totalTwist / 2f);

        for (int cLoop = 0; cLoop < cards.Count; cLoop++)
        {
            if (!cards[cLoop].isDragged)
            {
                RectTransform rt = cards[cLoop].GetComponent<RectTransform>();

                float twistForThisCard = startTwist + ((cLoop + 0.5f) * twistPerCard); // -- 0.5f to centre in middle of card
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, -twistForThisCard);

                float nudge = Mathf.Abs(twistForThisCard);
                nudge *= (0.01f * rt.rect.height); // ~10% card height

                if (hoveredCardIndex == cLoop)
                {
                    rt.localScale = Vector3.Lerp(rt.localScale, new Vector3(1.3f, 1.3f, 1.3f), Time.deltaTime * 10);

                    rt.rotation = Quaternion.Slerp(rt.rotation, Quaternion.identity, Time.deltaTime * 10);

                    Vector3 targetPos = handTransform.GetComponent<RectTransform>().position;
                    targetPos.x += cLoop * gapSize;
                    targetPos.z = 10;
                    rt.position = Vector3.Lerp(rt.position, targetPos, Time.deltaTime * drawSpeed);
                }
                else
                {
                    Vector3 targetPos = handTransform.GetComponent<RectTransform>().position;
                    targetPos.y -= nudge;
                    targetPos.x += cLoop * gapSize;
                    targetPos.z = cLoop;

                    if (hoveredCardIndex >= 0)
                    {
                        if (cLoop < hoveredCardIndex)
                        {
                            targetPos.x -= cLoop * gapSize * 0.1f;
                        }
                        else if (cLoop > hoveredCardIndex)
                        {
                            targetPos.x += rt.rect.width;
                        }
                    }

                    rt.rotation = Quaternion.Slerp(rt.rotation, targetRotation, Time.deltaTime * 10);
                    rt.position = Vector3.Lerp(rt.position, targetPos, Time.deltaTime * drawSpeed);
                    rt.localScale = Vector3.Lerp(rt.localScale, Vector3.one, Time.deltaTime * 10);
                }
            }
        }
    }

    public void AddCardToDiscardPile(CardSceneObj card)
    {
        owner.discardPile.Add(card.card);
        discardPileSizeText.text = owner.discardPile.Count.ToString();
        //card.gameObject.SetActive(false);

        Vector3 targetPos = Vector3.zero;
        RectTransform rt = card.GetComponent<RectTransform>();
        targetPos.z = 10000;
        rt.position = targetPos;
    }

    public void AddCardToExhaustPile(CardSceneObj card)
    {
        owner.exhaustPile.Add(card.card);
        exhausePileSizeText.text = owner.exhaustPile.Count.ToString();
        //card.gameObject.SetActive(false);

        Vector3 targetPos = Vector3.zero;
        RectTransform rt = card.GetComponent<RectTransform>();
        targetPos.z = 10000;
        rt.position = targetPos;
    }

    //public void FitCards()
    //{
    //    if (cardObjs.Count == 0) //if list is null, stop function
    //        return;

    //    GameObject img = cardObjs[0]; //Reference to first image in my list

    //    img.transform.position = start.position; //relocating my card to the Start Position
    //    img.transform.position += new Vector3((howManyAdded * gapFromOneItemToTheNextOne), 0, 0); // Moving my card 1f to the right
    //    img.transform.SetParent(HandDeck); //Setting ym card parent to be the Hand Panel

    //    cardObjs.RemoveAt(0);
    //    howManyAdded++;
    //}
}
