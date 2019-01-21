using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenDebug : MonoSingleton<OnScreenDebug>
{
    public GameObject logPrefab;
    public Transform container;

    public void Log(string _log)
    {
        GameObject newLog = GameObject.Instantiate(logPrefab, container);
        newLog.GetComponentInChildren<Text>().text = container.childCount + ": " + _log;
    }
}
