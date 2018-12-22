using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    GameController gc;
    //public GameObject cardPrefab;
    public List<CardSceneObj> cards = new List<CardSceneObj>(); // List that holds all my ten cards
    public Transform startTransform;  //Location where to start adding my cards
    public Transform handTransform; //The hand panel reference
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

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        //howManyAdded = 0;
        //gapFromOneItemToTheNextOne = 1.0f;
        handWidth = handTransform.GetComponent<RectTransform>().rect.width - gc.cardPrefab.GetComponent<RectTransform>().rect.width;
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

    public void UpdateDrag(PointerEventData eventData, CardSceneObj card)
    {
        // show target at mouse cursor.

        // keep card locked in highlighted position

        // -- show dotted curve between card and cursor
    }

    public void AddCard(CardSceneObj c)
    {
        RectTransform rt = c.GetComponent<RectTransform>();
        rt.position = startTransform.position;
        cards.Add(c);

        gapSize = handWidth / cards.Count;

        twistPerCard = totalTwist / cards.Count;
        startTwist = -1f * (totalTwist / 2f);

        c.SetHand(this);
    }

    void UpdateCards()
    {
        for(int cLoop = 0; cLoop < cards.Count; cLoop++)
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
