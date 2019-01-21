using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice", menuName = "Choice", order = 1)]
[System.Serializable]
public class Choice : ScriptableObject
{
    [HideInInspector]
    public bool isNew = true;
    public List<Property> requirements = new List<Property>();
    public string text;
    public List<Outcome> outcomes = new List<Outcome>();
}
