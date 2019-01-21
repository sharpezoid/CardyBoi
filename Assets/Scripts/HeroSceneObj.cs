using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSceneObj : MonoBehaviour
{
    public Hero hero;

    GameController gc;
    public Animator animator;

    public Deck initialDeck;
    public Deck currentDeck;

    public List<CardSceneObj> drawDeck = new List<CardSceneObj>();
    public List<CardSceneObj> discardPile = new List<CardSceneObj>();
    public List<CardSceneObj> exhaustPile = new List<CardSceneObj>();

    public StatUI statUI;

    public Hand hand;

    public void SetupHero(Hero h)
    {
        hero = h;

        gc = FindObjectOfType<GameController>();
        animator = GetComponent<Animator>();
    }

    void CreateDeck()
    {
        currentDeck = initialDeck;
        for (int cLoop = 0; cLoop < initialDeck.cards.Count; cLoop++)
        {
            CardSceneObj c = CardManager.instance.GetCard();
            RectTransform rt = c.GetComponent<RectTransform>();
            rt.SetParent(hand.handTransform);
            rt.position = hand.startTransform.position;
            c.GetComponent<CardSceneObj>().SetupCard(initialDeck.cards[cLoop]);
            drawDeck.Add(c.GetComponent<CardSceneObj>());
        }
    }

    public bool DrawCard()
    {
        if (drawDeck.Count == 0)
        {
            Debug.Log("Draw Card Fail : Nothing Left in draw deck");
            drawDeck.AddRange(discardPile.ToArray());
            discardPile.Clear();
        }

        if (drawDeck.Count == 0)
        {
            Debug.Log("Draw Card Fail : Nothing in draw deck or discard pile, the player has no cards (!!!???!)");
            return false;
        }

        CardSceneObj c = drawDeck[0];

        hand.AddCard(c);
        drawDeck.Remove(c);
        Debug.Log("CARD DRAWN! : " + c.card.Name);

        hand.UpdateDeckSizes();

        //statUI.SetStats(hero);

        return true;
    }
}
