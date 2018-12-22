using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Card/New Deck", order = 2)]
[System.Serializable]
public class Deck : ScriptableObject
{
    [SerializeField]
    public List<Card> cards = new List<Card>();
}
