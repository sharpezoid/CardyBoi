using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Connection : MonoBehaviour
{
    public OrbSlot A;
    public OrbSlot B;

    public LineRenderer lr;

    [ExecuteInEditMode]
    public void SetEnds(OrbSlot _A, OrbSlot _B)
    {
        Debug.Log("SET ENDS : " + _A + " - " + _B);

        A = _A;
        B = _B;

        lr.SetPosition(0, A.gameObject.transform.position);
        lr.SetPosition(1, B.gameObject.transform.position);
    }
}
