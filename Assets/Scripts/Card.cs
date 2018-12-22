﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/New Card", order = 1)]
[System.Serializable]
public class Card : ScriptableObject
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public string Description;

    [SerializeField]
    public int PlayCost;

    [SerializeField]
    public int BuyCost;

    [SerializeField]
    public enum SuitType
    {
        Gold, Blank, Clubs, Spades, Hearts, Diamonds, Chevrons, Torus, Star, Status, Darkness
    }
    public SuitType Suit = SuitType.Blank;
    [SerializeField]

    public List<CardEffect> effects = new List<CardEffect>();
}

[System.Serializable]
public class CardEffect
{
    public enum EffectEvent
    {
        OnDraw,
        OnPlay,
        OnDiscard,
        OnTrash,
        OnUpgrade,
        OnTaken,
        OnGiven,
        OnTurnStart,
        OnTurnEnd,
        OnDamageTaken,
        OnDamageDealt,
        OnHeal,
        OnSpellCast,
        OnAttack,
        OnDefend,
        OnGainLevel,
        OnDeath,
        OnKillingBlow,
        OnFirstBlood,
    }
    [SerializeField]
    public EffectEvent Event = EffectEvent.OnPlay;

    [SerializeField]
    public List<CardAttribute> attributes = new List<CardAttribute>();
}

[System.Serializable]
public class CardAttribute
{
    public enum Attribute
    {
        ModifyActions,
        DrawCards,
        BurnCards,
        DiscardCards,
        ModifyOpponentHealth,
        ModifyBlock,
        ModifyHealth,
        PoisonSelf,
        PoisonEnemy,//etc

    }
    public Attribute attribute = Attribute.DrawCards;

    public int Value = 0;
}