using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocationHand : MonoBehaviour
{
    public LocationDeck startingDeck;
    public LocationDeck currentDeck;
    public GameObject target;
    public List<LocationCardSceneObj> cards = new List<LocationCardSceneObj>(); // List that holds all my ten cards
    public Transform startTransform;  //Location where to start adding my cards
    public Transform handTransform; //The hand panel reference

    public Transform exhaustTransform;

    float gapSize; //the gap I need between each card
    float handWidth; // how wide is a hand 
    float drawSpeed = 10;
    public const int MAX_HAND_SIZE = 10;

    public LocationCardSceneObj hoveredCard = null;
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


    void Start()
    {
        handWidth = handTransform.GetComponent<RectTransform>().rect.width - GameController.instance.locationCardPrefab.GetComponent<RectTransform>().rect.width;

        currentDeck = startingDeck;
        for (int cLoop = 0; cLoop < currentDeck.cards.Count; cLoop++)
        {
            GameObject newCard = GameObject.Instantiate(GameController.instance.locationCardPrefab, handTransform);
            LocationCardSceneObj c = newCard.GetComponent<LocationCardSceneObj>();
            RectTransform rt = c.GetComponent<RectTransform>();
            rt.position = startTransform.position;
            c.GetComponent<LocationCardSceneObj>().SetupCard(currentDeck.cards[cLoop]);
            cards.Add(c);
            c.SetHand(this);
        }

        UpdateDeckSizes();
    }

    public void UpdateDeckSizes()
    {
        drawDeckSizeText.text = currentDeck.cards.Count.ToString();
    }

    private void Update()
    {
        UpdateCards();
    }

    public void SetHoveredCard(LocationCardSceneObj card)
    {
        if (card == null || card.isDragged)
        {
            hoveredCard = null;
            hoveredCardIndex = -1;
        }
        else
        {
            hoveredCardIndex = cards.IndexOf(card);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.SetSiblingIndex(i);
            }
            hoveredCard = card;
            prevHoverIndex = hoveredCardIndex;
            cards[prevHoverIndex].gameObject.transform.SetAsLastSibling();
        }
    }

    public void StartDrag(PointerEventData eventData, LocationCardSceneObj card)
    {
        goneBeyondThreshold = false;
    }

    public void UpdateDrag(PointerEventData eventData, LocationCardSceneObj card)
    {
        // show target at mouse cursor.
        target.GetComponent<RectTransform>().position = eventData.position;

        // keep card locked in highlighted position

        // -- what are we over? show color change.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.gameObject.tag == "CardSlot")
            {

                    // hit a valid slot
                    target.GetComponent<Image>().color = Color.green;

            }
            else
            {
                // hit nowt
                target.GetComponent<Image>().color = Color.red;
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



    public void EndDrag(PointerEventData eventData, LocationCardSceneObj card)
    {
        Debug.Log("End Drag");

        // -- what are we over?
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        hits = Physics.SphereCastAll(ray, 1, 1000);

        for(int hLoop = 0; hLoop < hits.Length; hLoop++) 
        {
            RaycastHit hit = hits[hLoop];
            Debug.Log("HIT : " + hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "CardSlot")
            {
                if (hit.transform.gameObject.GetComponent<LocationSlot>().card == null)
                {
                    PlayCard(card, hit.transform.gameObject.GetComponent<LocationSlot>());
                    // -- send to discard pile
                    AddCardToDiscardPile(card);

                    break;
                }
            }
            else
            {
                // -- fail to play...
            }
            
        }

        card.isDragged = false;

        UpdateDeckSizes();
    }

    //public void DiscardHand()
    //{
    //    for (int i = cards.Count - 1; i >= 0; i--)
    //    {
    //        Debug.Log("Discarding CARD : " + cards[i].card.Name + " i: " + i);
    //        AddCardToDiscardPile(cards[i]);
    //    }
    //}

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

    public void AddCardToDiscardPile(LocationCardSceneObj card)
    {   
        //owner.discardPile.Add(card.card);
        //discardPileSizeText.text = owner.discardPile.Count.ToString();
        ////card.gameObject.SetActive(false);

        //Vector3 targetPos = Vector3.zero;
        //RectTransform rt = card.GetComponent<RectTransform>();
        //targetPos.z = 10000;
        //rt.position = targetPos;
    }

    void PlayCard(LocationCardSceneObj card, LocationSlot slot)
    {
        Debug.Log("PLAY CARD");
        card.isWaitingToPlay = true;
        slot.card = card;
        card.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(slot.gameObject.transform.position);
        EncounterController.instance.StartEncounter(card.card.encounter);
    }
}
