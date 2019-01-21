using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Outcome", menuName = "Outcome", order = 1)]
[System.Serializable]
public class Outcome : ScriptableObject
{
    public int chance = 100;

    public Stage stage;

    // -- stuff this has effect on...
    public List<AttributeMod> attributeMods = new List<AttributeMod>();

    public bool alive = true;
}
