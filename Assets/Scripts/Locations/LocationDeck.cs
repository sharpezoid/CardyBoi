using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Deck", menuName = "Locations/New Location Deck", order = 2)]
[System.Serializable]
public class LocationDeck : ScriptableObject
{
    [SerializeField]
    public List<LocationCard> cards = new List<LocationCard>();
}