using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Card", menuName = "Locations/New Location Card", order = 2)]
[System.Serializable]
public class LocationCard : ScriptableObject
{
    public string locationName;
    public string locationDescription;

    public Encounter encounter;
}


