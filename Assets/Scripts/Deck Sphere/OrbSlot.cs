using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSlot : MonoBehaviour
{
    public List<OrbSlot> connectedOrbs = new List<OrbSlot>();

    public enum SlotType
    {
        Empty,
        Card,
        Power,
        Stat,
        Lock,
    }
    public SlotType orbType = SlotType.Empty;

    [ExecuteInEditMode]
    void CheckConnectedOrbs()
    {
        for (int cLoop = 0; cLoop < connectedOrbs.Count; cLoop++)
        {
            if (connectedOrbs[cLoop] != null)
            {
                // -- check that our supposed connections are also attached to us, so needy.
                if (!connectedOrbs[cLoop].connectedOrbs.Contains(this))
                {
                    connectedOrbs[cLoop].connectedOrbs.Add(this);
                }
            }
        }
    }
}
