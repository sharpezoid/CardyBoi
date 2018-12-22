using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    GameController gc;
    public Animator animator;
    public Deck initialDeck;
    public List<CardSceneObj> drawDeck = new List<CardSceneObj>();
    public List<CardSceneObj> discardPile = new List<CardSceneObj>();
    public List<CardSceneObj> destroyPile = new List<CardSceneObj>();
    public Hand hand;

    public int maxDraw = 4;

    public enum Class
    {
        Warrior,        // -- default basic class
        Rogue,          // -- high dmg evade tank class
        Wizard,         // -- high utility mage
        WhiteMage,      // -- healbot
        BlackMage,      // -- glass cannon  
        Fighter,        // -- melee dmg
        Paladin,        // -- tank w/ heals
        Summoner,       // -- minions
        Hunter,         // -- ranged dmg, traps
        Assassin        // -- melee dmg criticals

        // -- HOW CLASSES BEHAVE MECHANICALLY...
        // -- oil > fire type chains for damage.
        // -- control deck
        // -- poison deck / shiv deck
        //
        //
        //
        //
        //
    }

    public void SetupHero()
    {
        gc = FindObjectOfType<GameController>();
        animator = GetComponent<Animator>();

        CreateDeck();
    }

    void CreateDeck()
    {
        for (int cLoop = 0; cLoop < initialDeck.cards.Count; cLoop++)
        {
            GameObject c = GameObject.Instantiate(gc.cardPrefab, hand.startTransform);
            RectTransform rt = c.GetComponent<RectTransform>();
            rt.position = hand.startTransform.position;
            c.GetComponent<CardSceneObj>().SetupCard(initialDeck.cards[cLoop]);

            drawDeck.Add(c.GetComponent<CardSceneObj>());
        }
    }

    public void DrawCard()
    {
        CardSceneObj c = drawDeck[0];
        hand.AddCard(c);
        drawDeck.RemoveAt(0);
    }
}
