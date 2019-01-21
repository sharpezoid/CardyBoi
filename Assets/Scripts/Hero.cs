using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero : Unit
{
    //[SerializeField]
    //GameController gc;
    //[SerializeField]
    //public Animator animator;

    [SerializeField]
   // public Deck initialDeck;
   // public Deck currentDeck;
    public List<Card> drawDeck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> exhaustPile = new List<Card>();
    //public List<CardSceneObj> drawDeck = new List<CardSceneObj>();
    //public List<CardSceneObj> discardPile = new List<CardSceneObj>();
    //public List<CardSceneObj> exhaustPile = new List<CardSceneObj>();
    //public Hand hand;

    public int maxDraw = 3;
    public int actions = 1;
    public int maxActions = 1;
    public int mana = 0;
    public int maxMana = 0;

    //public StatUI statUI;

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

    public void ResetTurn()
    {
        actions = maxActions;
    }


}
