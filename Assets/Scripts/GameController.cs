using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //CONVENIENCE VARIABLES
    UIDynamicElements uiElements;
    MenuManager menuManager;

    //
    private List<string> targetList = new List<string>() { "Sol", "Mercurio", "Venus", "Tierra", "Luna", "Marte", "Júpiter", "Saturno", "Urano", "Neptuno", "Pluto" };

    private string targetToFind;
    private int lives = 3;
    private float timeInSeconds = 60;
    private float targetDefaultValue = 10000;
    private float targetCurrentValue;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Instance = this;
        }
        uiElements = UIDynamicElements.Instance;
        menuManager = MenuManager.Instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideAllUI();
        //BeginGame();


        //Timer.Instance.SetSeconds(timeInSeconds);

        ShowAllUI();
        GenerateNextTarget();
        UpdateUI();
    }

    

    void BeginGame()
    {
        uiElements.startAnimation();
    }
    void ShowAllUI()
    {
        uiElements.ShowAllUI();
    }
    void HideAllUI()
    {
        uiElements.HideAllUI();
    }
    void UpdateUI()
    {
        uiElements.TargetNameTextChange("" + targetToFind);
        uiElements.LivesNumberTextChange("" + lives);
        uiElements.ScoreNumberTextChange("" + score.ToString("000000"));
    }


    public void OnImageTargetFound(string targetFound)
    {
        //DEBUGGING
        uiElements.ScoreNumberTextChange(targetFound);

        if (targetFound.Equals(targetToFind))
        {
            //Correct answer
            score += 100;
            GenerateNextTarget();
        } else
        {
            lives--;
            if (lives == 0)
            {
                GameOver();
            }
        }

        UpdateUI();
    }

    void GenerateNextTarget()
    {
        targetCurrentValue = targetDefaultValue;

        int randomPosition = Random.Range(0, targetList.Count);
        targetToFind = targetList[randomPosition];
    }

    void GameOver()
    {
        uiElements.TargetSupertextChange("");
        uiElements.TargetNameTextChange("FIN DE LA PARTIDA");
    }

    public void ReturnToMainMenu()
    {
        menuManager.MainMenuScreen();
    }
}
