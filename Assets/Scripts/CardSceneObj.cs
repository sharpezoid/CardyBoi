using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSceneObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public bool MouseOver = false;

    Hand hand = null;

    public Card card;

    public Text cardNameText;
    public Text cardDescriptionText;
    public Image cardImage;
    public Text cardActionText;
    public Text cardCostText;

    public bool isDragged = false;

    public bool isWaitingToPlay = false;

    public void SetupCard(Card _card)
    {
        card = _card;
        cardNameText.text = _card.Name;
        cardDescriptionText.text = _card.Description;
        cardActionText.text = GenerateCardText(_card.effects);
        cardCostText.text = _card.ActionCost.ToString();
    }

    public IEnumerator PlayCard()
    {
        Debug.Log("Card Scene Object Play Card : " + card.Name + "  -  " + card.Description );

        for (int eLoop = 0; eLoop < card.effects.Count; eLoop++)
        {
            GameObject cardEffect = GameObject.Instantiate(card.effects[eLoop].cardEffectPrefab);

            yield return StartCoroutine(cardEffect.GetComponent<TestEffect>().Play(card));

            Destroy(cardEffect);

            yield return null;
        }



        Debug.Log("Done Playing Card " + card.Name);

        yield return null;
    }


    string GenerateCardText(List<CardEffect> effects)
    {
        StringBuilder sb = new StringBuilder();

        for (int eLoop = 0; eLoop < effects.Count; eLoop++)
        {
            switch (effects[eLoop].Event)
            {
                case CardEffect.EffectEvent.OnAttack:   sb.Append("When attacking ");   break;
                case CardEffect.EffectEvent.OnDamageDealt: sb.Append("Every time damage is dealt "); break;
                case CardEffect.EffectEvent.OnDamageTaken: sb.Append("When damage is taken "); break;
                case CardEffect.EffectEvent.OnDeath: sb.Append("On hero demise "); break;
                case CardEffect.EffectEvent.OnDefend: sb.Append("When defending "); break;
                case CardEffect.EffectEvent.OnDiscard: sb.Append("On discard "); break;
                case CardEffect.EffectEvent.OnDraw: sb.Append("When drawn "); break;
                case CardEffect.EffectEvent.OnFirstBlood: sb.Append("First Blood (CANT DECIDE!)"); break;
                case CardEffect.EffectEvent.OnGainLevel: sb.Append("On level up "); break;
                case CardEffect.EffectEvent.OnGiven: sb.Append("When given card "); break;
                case CardEffect.EffectEvent.OnHeal: sb.Append("Upon heal "); break;
                case CardEffect.EffectEvent.OnKillingBlow: sb.Append("When enemy dies "); break;
                case CardEffect.EffectEvent.OnPlay: sb.Append("On play "); break;
                case CardEffect.EffectEvent.OnSpellCast: sb.Append("On spell cast "); break;
                case CardEffect.EffectEvent.OnTaken: sb.Append("When taken "); break;
                case CardEffect.EffectEvent.OnTrash: sb.Append("When card is trashed "); break;
                case CardEffect.EffectEvent.OnTurnEnd: sb.Append("On turn end "); break;
                case CardEffect.EffectEvent.OnTurnStart: sb.Append("On turn start "); break;
                case CardEffect.EffectEvent.OnUpgrade: sb.Append("When upgraded "); break;
            }
            for (int aLoop = 0; aLoop < effects[eLoop].attributes.Count; aLoop++)
            {
                CardAttribute atb = effects[eLoop].attributes[aLoop];
                switch (atb.attribute)
                {
                    case CardAttribute.Attribute.BurnCards: sb.AppendLine("burn " + atb.Value.ToString() + " cards"); break;
                    case CardAttribute.Attribute.DiscardCards: sb.AppendLine("discard " + atb.Value.ToString() + " card(s)"); break;
                    case CardAttribute.Attribute.DrawCards: sb.AppendLine("draw " + atb.Value.ToString() + " card(s)"); break;
                    case CardAttribute.Attribute.ModifyActions: sb.AppendLine("add " + atb.Value.ToString() + " actions"); break;
                    case CardAttribute.Attribute.ModifyBlock: sb.AppendLine("add " + atb.Value.ToString() + " block"); break;
                    case CardAttribute.Attribute.ModifyHealth: sb.AppendLine( ( atb.Value >= 0 ? "gain " : "lose ") + atb.Value.ToString() + " health"); break;
                    case CardAttribute.Attribute.ModifyOpponentHealth: sb.AppendLine( "deal " + atb.Value.ToString() + " damage"); break;
                    case CardAttribute.Attribute.PoisonEnemy: sb.AppendLine("Poison enemy for " + atb.Value.ToString()); break;
                    case CardAttribute.Attribute.PoisonSelf: sb.AppendLine("Poison self for " + atb.Value.ToString()); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break; MORE TO COME!!
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;
                    //case CardAttribute.Attribute.BurnCards: sb.Append(" burn " + atb.Value.ToString() + " cards"); break;

                        //etc etc
                }
            }
            //retVal += effects[i].attributes.Count
        }

        return sb.ToString();
    }

    public void SetHand(Hand _hand)
    {
        hand = _hand;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hand || isDragged || isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards ) { return; }

        hand.SetHoveredCard(this);
        //MouseOver = true;
    }

    public void OnPointerStay(PointerEventData eventData)
    {
        if (!hand || isDragged || isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards) { return; }
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
        if (!isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards)
        {
            isDragged = true;
            hand.StartDrag(eventData, this);
            //hand.SetHoveredCard(null);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!hand || isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards) { return; }

        hand.EndDrag(eventData, this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!hand || isWaitingToPlay || TurnController.instance.turnState != TurnController.TurnState.PlayCards) { return; }

        hand.EndDrag(eventData, this);
    }
}
