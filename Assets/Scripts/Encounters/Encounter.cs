using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "Encounter", order = 1)]
[System.Serializable]
public class Encounter : ScriptableObject
{
    [HideInInspector]
    public bool isNew = true;
    public List<Stage> stages = new List<Stage>();

    public Stage currentStage;
}



