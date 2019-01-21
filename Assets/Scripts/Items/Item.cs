using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
[System.Serializable]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;

    public List<AttributeMod> attributes = new List<AttributeMod>();
}

[System.Serializable]
public class AttributeMod
{
    public int intValue;
    public string stringValue;

    public enum AttributeType
    {
        NONE,
        UNIT_STATUS_FLAGS,
        HERO_CLASS,
        NEXT_ENCOUNTER_STAGE,
        NEXT_ENCOUNTER_STAGE_CHOICE,
        ORBS,
        ITEMS,
        CURRENT_HEALTH,
        MAX_HEALTH,
        ACTIONS,
        MAX_ACTIONS,
        MANA,
        MAX_MANA,
        GOLD,
        GOLD_MULTIPLIER,
        MAX_DRAW,
        STRENGTH,
        SPEED,
        STAMINA,
        MAGIC,
        ATTACK,
        DEFENCE,
        DODGE,
        SPELL_DEFENCE,
        SPELL_DODGE
    }
    public AttributeType type = AttributeType.NONE;

    public AttributeMod(int _intVal, string _stringVal)
    {
        intValue = _intVal;
        stringValue = _stringVal;
    }

    public void ApplyAttributes(Hero target)
    {
        Debug.Log("DOING AN APPLY ATTRIBUTES, THIS IS NOT COMPLETE YET - #FIXME #TODO");
        switch (type)
        {
           // case AttributeType.UNIT_STATUS_FLAGS: target.ApplyStatus(intValue); break; #FIXME #TODO
           // case AttributeType.HERO_CLASS:  target.ChangeClass(intValue);       break;
            case AttributeType.NEXT_ENCOUNTER_STAGE: break;
            case AttributeType.NEXT_ENCOUNTER_STAGE_CHOICE: break;
            case AttributeType.ORBS: break;
            case AttributeType.ITEMS: break;
            case AttributeType.CURRENT_HEALTH: break;
            case AttributeType.MAX_HEALTH: break;
            case AttributeType.ACTIONS: break;
            case AttributeType.MAX_ACTIONS: break;
            case AttributeType.MANA: break;
            case AttributeType.MAX_MANA: break;
            case AttributeType.GOLD: break;
            case AttributeType.GOLD_MULTIPLIER: break;
            case AttributeType.MAX_DRAW: break;
            case AttributeType.STRENGTH: break;
            case AttributeType.SPEED: break;
            case AttributeType.STAMINA: break;
            case AttributeType.MAGIC: break;
            case AttributeType.ATTACK: break;
            case AttributeType.DEFENCE:                                    break;
            case AttributeType.DODGE: break;
            case AttributeType.SPELL_DEFENCE: break;
            case AttributeType.SPELL_DODGE:
                break;

                case AttributeType.NONE:
                        default:
                break;
        }
    }
}