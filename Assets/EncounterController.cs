using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoSingleton<EncounterController>
{
    public Encounter currentEncounter;

    public GameObject encounterStagePanelPrefab;
    public Transform encounterStagePanelParent;
    public GameObject currentStagePanel;
    public GameObject prevStagePanel;

    public GameObject choiceButtonPrefab;
    public Transform choiceButtonParent;

    private void Start()
    {
        currentStagePanel = GameObject.Instantiate(encounterStagePanelPrefab, encounterStagePanelParent);
        prevStagePanel = GameObject.Instantiate(encounterStagePanelPrefab, encounterStagePanelParent);

        StartEncounter(currentEncounter);
    }

    public void StartEncounter(Encounter encounter)
    {
        currentEncounter = encounter;

        if (currentEncounter.stages.Count == 0) { return; }

        currentEncounter.currentStage = currentEncounter.stages[0];

        StartStage(currentEncounter.currentStage);
    }

    public void StartStage(Stage stage)
    {
        currentEncounter.currentStage = stage;

        GameObject nextPanel = prevStagePanel;
        EncounterUI newStageUI = nextPanel.GetComponent<EncounterUI>();
        prevStagePanel = null;
        prevStagePanel = currentStagePanel;
        currentStagePanel = null;
        currentStagePanel = nextPanel;
        
        newStageUI.SetEncounterStage(currentEncounter.currentStage);
    }

    public void EndEncounter()
    {
        Debug.Log("Encounter Over");
        currentEncounter = null;
    }

    private void Update()
    {
        if (!currentEncounter)
        {
            currentStagePanel.GetComponent<RectTransform>().position = Vector3.Lerp(currentStagePanel.GetComponent<RectTransform>().position, new Vector3(0, 0, 0), Time.deltaTime);
            prevStagePanel.GetComponent<RectTransform>().position = Vector3.Lerp(prevStagePanel.GetComponent<RectTransform>().position, new Vector3(0, 0, 0), Time.deltaTime);
        }
        else
        {
            if (currentStagePanel)
            {
                currentStagePanel.GetComponent<RectTransform>().position = Vector3.Lerp(currentStagePanel.GetComponent<RectTransform>().position, new Vector3(0, 1080, 0), Time.deltaTime);
            }

            if (prevStagePanel)
            {
                prevStagePanel.GetComponent<RectTransform>().position = Vector3.Lerp(prevStagePanel.GetComponent<RectTransform>().position, new Vector3(0, 0, 0), Time.deltaTime);
            }
        }
    }
}
