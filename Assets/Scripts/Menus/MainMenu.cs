using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoSingleton<MainMenu>
{
    public Button gameButton;
    public Button abandonButton;

    string path;

    bool savedGame = false;

    public List<Hero> heroes = new List<Hero>();


    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "SquadData/SQUAD.txt");
        if (File.Exists(path))
        {
            abandonButton.gameObject.SetActive(true);
            gameButton.gameObject.GetComponentInChildren<Text>().text = "Continue Game";
            gameButton.onClick.RemoveAllListeners();
            gameButton.onClick.AddListener(ContinueGame);
            abandonButton.onClick.AddListener(AbandonGame);
        }
        else
        {
            abandonButton.gameObject.SetActive(false);
            gameButton.gameObject.GetComponentInChildren<Text>().text = "Start New Game";
            gameButton.onClick.RemoveAllListeners();
            gameButton.onClick.AddListener(StartNewGame);
        }
    }

    public void CheckForSaveGame()
    {

    }

    void StartNewGame()
    {
        Debug.Log("Start New Game");
    }

    void ContinueGame()
    {
        Debug.Log("CONTINUE GAME");
        heroes = LoadSquad(path);
        for (int i = 0; i < heroes.Count; i++)
        {
            Debug.Log("Loaded Hero " + i + "   actions : " + heroes[i].actions);
            GameController.instance.AddHero(heroes[i]);
        }

        SceneManager.LoadScene("Location Map", LoadSceneMode.Additive);

        Destroy(gameObject);
    }

    void AbandonGame()
    {
        Debug.Log("Abandon Game");
    }

    static List<Hero> LoadSquad(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (List<Hero>)binaryFormatter.Deserialize(fileStream);
        }
    }
}
