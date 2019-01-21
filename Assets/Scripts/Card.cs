using System;
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
    public int ActionCost;

    [SerializeField]
    public int ManaCost;

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
    [Flags]
    public enum Targets : byte    // -- what can this card target? enemy, self, all, ally ?
    {
        Self = 0,
        Ally = 1,
        Enemy = 2,
        All = 4,
        None = 8       
    };
    [SerializeField]
    public readonly Targets TargetFlags;

    [SerializeField]
    public bool exhaust = false;
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

    [SerializeField]
    public GameObject cardEffectPrefab;
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