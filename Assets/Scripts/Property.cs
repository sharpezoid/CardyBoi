using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// property in game that we want to check for.
public class Property
{
    private string Name;
    private int Value;
    private QueryProperty Activation;
    private int ActivationValue;
    private int InitialValue;

    public Property(string _Name, int _Value, QueryProperty _Activation, int _ActivationValue)
    {
        Name = _Name;
        Value = _Value;
        Activation = _Activation;
        ActivationValue = _ActivationValue;
    }

    public bool IsActive()
    {
        bool retVal = false;

        switch (Activation)
        {
            case QueryProperty.ACTIVE_IF_GREATER_THAN:          retVal = Value > ActivationValue;   break;
            case QueryProperty.ACTIVE_IF_EQUAL:                 retVal = Value == ActivationValue;  break;
            case QueryProperty.ACTIVE_IF_GREATER_THAN_OR_EQUAL: retVal = Value >= ActivationValue;  break;
            case QueryProperty.ACTIVE_IF_LESS_THAN:             retVal = Value < ActivationValue;   break;
            case QueryProperty.ACTIVE_IF_LESS_THAN_OR_EQUAL:    retVal = Value <= ActivationValue;  break;
        }

        return retVal;
    }
}

public enum QueryProperty
{
    ACTIVE_IF_GREATER_THAN,
    ACTIVE_IF_GREATER_THAN_OR_EQUAL,
    ACTIVE_IF_EQUAL,
    ACTIVE_IF_LESS_THAN,
    ACTIVE_IF_LESS_THAN_OR_EQUAL
}