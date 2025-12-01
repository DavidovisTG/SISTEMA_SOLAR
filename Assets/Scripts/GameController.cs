using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static MenuManager MenuManager;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI targetNameSuperText;
    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI scoreText;

    private List<string> targetList = new List<string>() { "Sol", "Mercurio", "Venus", "Tierra", "Luna", "Marte", "Júpiter", "Saturno", "Urano", "Neptuno", "Pluto" };

    private string targetToFind;
    private int lives = 3;

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
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateNextTarget();
        UpdateUI();
    }

    void UpdateUI()
    {
        targetNameText.text = "" + targetToFind;
        livesText.text = "" + lives;
        scoreText.text = "" + score.ToString("000000");
    }

    public void OnImageTargetFound(string targetFound)
    {
        //DEBUGGING
        scoreText.text = targetFound;

        if (targetFound == targetToFind)
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
        int randomPosition = Random.Range(0, targetList.Count);
        targetToFind = targetList[randomPosition];
    }

    void GameOver()
    {
        targetNameText.text = "FIN DE LA PARTIDA";
    }

    public void ReturnToMainMenu()
    {
        MenuManager.Instance.MainMenuScreen();
    }
}
