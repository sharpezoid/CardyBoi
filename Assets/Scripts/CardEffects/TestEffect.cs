using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -- TEST EFFECT CLASS!!!
public partial class TestEffect : MonoBehaviour, ISampleInterface
{
    public IEnumerator Play(Card card)
    {
        GetComponent<TextMesh>().text = card.Name;
        // -- 
        yield return new WaitForSeconds(1);

        Debug.Log("TEST EFFECT OVER");

        yield return null;
    }
}

interface ISampleInterface
{
    IEnumerator Play(Card card);
}
