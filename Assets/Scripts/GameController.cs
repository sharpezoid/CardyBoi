using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public Squad squad;

    public GameObject cardPrefab;

    private void Awake()
    {
        for (int i = 0; i < squad.heroes.Count; i++)
        {
            squad.heroes[i].SetupHero();
        }

        OnDrawAction += OnDrawActionDefault;
        OnPlayCardAction += OnPlayCardActionDefault;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            FindObjectOfType<TurnController>().DoTurn();
        }
    }

    //OnDraw
    public delegate void OnDrawActionDelegate();
    public OnDrawActionDelegate OnDrawAction;
    void OnDrawActionDefault()
    {
        print("OnDrawActionCalled");
    }

    //OnPlayCard
    public delegate void OnPlayCardActionDelegate();
    public OnPlayCardActionDelegate OnPlayCardAction;
    void OnPlayCardActionDefault()
    {
        print("OnPlayCardActionCalled");
    }

    //OnDiscard

    //OnTrashCard

    //OnUpgradeCard

    //OnCardTaken

    //OnCardGiven

    //OnTurnStart

    //OnTurnEnd

    //OnDamageTaken

    //OnDamageDealt

    //OnHeal

    //OnSpellCast

    //OnAttack

    //OnDefend

    //OnGainLevel

    //OnDeath

    //OnKillingBlow

    //OnFirstBlood


}