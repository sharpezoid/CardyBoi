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

    Card card;

    public Text cardName;
    public Text cardDescription;
    public Image cardImage;
    public Text cardAction;
    public Text cardCost;

    public bool isDragged = false;

    public void SetupCard(Card _card)
    {
        card = _card;
        cardName.text = _card.Name;
        cardDescription.text = _card.Description;
        cardAction.text = GenerateCardText(_card.effects);
        cardCost.text = _card.PlayCost.ToString();
    }

    public IEnumerator PlayCard()
    {
        // -- play the card animation 
        yield return new WaitForSeconds(1.5f);

        for (int eLoop = 0; eLoop < card.effects.Count; eLoop++)
        {
            for (int aLoop = 0; aLoop < card.effects[eLoop].attributes.Count; aLoop++)
            {
                CardAttribute atb = card.effects[eLoop].attributes[aLoop];
                switch (atb.attribute)
                {
                    case CardAttribute.Attribute.BurnCards:
                        // play a burn sequence...
                        break;

                    case CardAttribute.Attribute.DiscardCards:
                        // play a discard sequence
                        break;
                    case CardAttribute.Attribute.DrawCards:
                        // draw card sequence
                        break;

                    case CardAttribute.Attribute.ModifyActions:
                        // add action sequence
                        break;

                    case CardAttribute.Attribute.ModifyBlock:  break;
                    case CardAttribute.Attribute.ModifyHealth:  break;
                    case CardAttribute.Attribute.ModifyOpponentHealth: break;
                    case CardAttribute.Attribute.PoisonEnemy: break;
                    case CardAttribute.Attribute.PoisonSelf:
                        break;
                }
            }
        }
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
        if (!hand || isDragged ) { return; }

        hand.SetHoveredCard(this);
        //MouseOver = true;
    }

    public void OnPointerStay(PointerEventData eventData)
    {
        if (!hand || isDragged ) { return; }
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
        hand.UpdateDrag(eventData, this);
        //GetComponent<RectTransform>().transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG START");
        isDragged = true;
        //hand.SetHoveredCard(null);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG END");
        isDragged = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
