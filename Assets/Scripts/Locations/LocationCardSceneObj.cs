using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocationCardSceneObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public bool MouseOver = false;

    LocationHand hand = null;

    public LocationCard card;

    public Text cardNameText;
    public Text cardDescriptionText;
    public Image cardImage;

    public bool isDragged = false;
    public bool isWaitingToPlay = false;

    public void SetupCard(LocationCard _card)
    {
        card = _card;
        cardNameText.text = _card.locationName;
        cardDescriptionText.text = _card.locationDescription;
    }

    public IEnumerator PlayCard()
    {
        Debug.Log("Card Scene Object Play Card : " + card.locationName);

        yield return null;
    }


    
    public void SetHand(LocationHand _hand)
    {
        hand = _hand;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hand || isDragged || isWaitingToPlay ) { return; }

        hand.SetHoveredCard(this);
        //MouseOver = true;
    }

    public void OnPointerStay(PointerEventData eventData)
    {
        if (!hand || isDragged || isWaitingToPlay ) { return; }
        //MouseOver = true;
        hand.SetHoveredCard(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hand) { return; }

        hand.SetHoveredCard(null);
        //MouseOver = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragged && !isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards) // -- something else might force a stop
        {
            hand.UpdateDrag(eventData, this);
        }
        //GetComponent<RectTransform>().transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isWaitingToPlay)
        {
            isDragged = true;
            hand.StartDrag(eventData, this);
            //hand.SetHoveredCard(null);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!hand || isWaitingToPlay) { return; }

        hand.EndDrag(eventData, this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!hand || isWaitingToPlay) { return; }

        hand.EndDrag(eventData, this);
    }
}
