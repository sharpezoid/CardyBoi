using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    public Image background;
    public Text title;
    public Text description;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonParent;
    Stage stage;

    int stageIndex = -1;
    public void SetEncounterStage(Stage _stage)
    {
        stage = _stage;

        for (int i = choiceButtonParent.childCount -1; i > 0; i--)
        {
            Destroy(choiceButtonParent.GetChild(i).gameObject);
        }

        // background.sprite = stage.background;
        title.text = stage.Name;
        description.text = stage.Text;

        stageIndex = -1;

        for (int bLoop = 0; bLoop < stage.choices.Count; bLoop++)
        {
            Debug.Log("bloopp : " + bLoop);
            GameObject newButton = GameObject.Instantiate(choiceButtonPrefab, choiceButtonParent);
            AddListener(newButton.GetComponent<Button>(), bLoop);
           // newButton.GetComponent<Button>().onClick.AddListener(TaskOnClick);
            //newButton.GetComponent<Button>().onClick.AddListener(delegate { SelectChoice(bLoop); });
            //newButton.GetComponent<Button>().onClick.AddListener(() => SelectChoice());
            newButton.GetComponentInChildren<Text>().text = stage.choices[bLoop].text;
        }
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }

    void AddListener(Button b, int index)
    {
        b.onClick.AddListener(() => SelectChoice(index));
    }

    void SelectChoice(object userData)
    {
        stageIndex = (int)userData;
        Debug.Log("index : " + stageIndex); 
        Choice choice = stage.choices[stageIndex];


        int randomWeight = Random.Range(0, 100);
        Outcome o = null;
        for (int cLoop = 0; cLoop < choice.outcomes.Count; cLoop++)
        {
            randomWeight -= choice.outcomes[cLoop].chance;

            if (randomWeight <= 0)
            {
                o = choice.outcomes[cLoop];

                break;
            }
        }

        if(o != null)
        {
            if (o.stage != null)
            {
                EncounterController.instance.StartStage(o.stage);
            }
        }
        //-- else no stage so end encountere
        else
        {
            EncounterController.instance.EndEncounter();
        }
    }
}
