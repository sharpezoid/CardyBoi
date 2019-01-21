using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoSingleton<GameController>
{
    public List<HeroSceneObj> squad = new List<HeroSceneObj>();
    public void AddHero(Hero h)
    {
        GameObject newSquady = GameObject.Instantiate(heroPrefab);
        newSquady.GetComponent<HeroSceneObj>().SetupHero(h);
    }
    public List<LocationCard> locationCards = new List<LocationCard>();

    public GameObject locationCardPrefab;
    public GameObject cardPrefab;
    public GameObject heroPrefab;
    public GameObject heroUI;

    private void Awake()
    {
        OnDrawAction += OnDrawActionDefault;
        OnPlayCardAction += OnPlayCardActionDefault;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSquad(squad, Path.Combine(Application.persistentDataPath, "CharacterData.txt"));
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
    const string folderName = "SquadData";
    const string fileExtension = ".txt";
    static void SaveSquad(List<HeroSceneObj> data, string path)
    {
        Debug.Log("SAVE SQUAD ");

        List<Hero> heroes = new List<Hero>();
        for(int hLoop =0; hLoop < data.Count; hLoop++)
        {
            heroes.Add(data[hLoop].hero);
        }

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string dataPath = Path.Combine(folderPath, "SQUAD" + fileExtension);

        Debug.Log("Data Path : " + dataPath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, heroes);
        }
    }
}