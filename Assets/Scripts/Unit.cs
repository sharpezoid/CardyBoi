using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health = 100;
    public int defence = 0;

    public enum UnitStatus
    {
        None,
        Confused,
        Poison,
        Doomed,
        Weak,
        Haste,
        Slow,
        Hate,
        Rage,
        Scared,
        Broken,
        Unconscious,
        Asleep,
        Transformed
    }
    public UnitStatus unitStatus = UnitStatus.None;
}
