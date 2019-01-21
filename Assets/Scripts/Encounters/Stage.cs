using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter Stage", menuName = "Encounter Stage", order = 1)]
[System.Serializable]
public class Stage : ScriptableObject
{
    [HideInInspector]
    public bool isNew = true;
    public Sprite background;
    public string Name;
    public string Text;
    public List<Choice> choices = new List<Choice>();
}
