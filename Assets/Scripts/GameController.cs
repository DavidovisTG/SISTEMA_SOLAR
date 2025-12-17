using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //CONVENIENCE VARIABLES
    UIDynamicElements uiElements;

    //
    private List<string> targetList = new List<string>() { "Sol", "Mercurio", "Venus", "Tierra", "Luna", "Marte", "Júpiter", "Saturno", "Urano", "Neptuno", "Pluto" };
    private List<string> seenTargetsList = new List<string>();

    private string targetToFind;
    private int lives = 3;
    private float timerTimeInSeconds = 60;
    private float targetDefaultValue = 10000;
    private float targetCurrentValue;
    private int score = 0;

    private bool gameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "GameScene") return;
        Debug.LogError("PASA POR AQUI");
        uiElements = FindFirstObjectByType<UIDynamicElements>();
        uiElements.AllUIVisible(false);
        StartCoroutine(BeginGame());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    IEnumerator BeginGame()
    {
        yield return uiElements.startAnimation();
        uiElements.AllUIVisible(true);

        uiElements.SetTimer(timerTimeInSeconds);
        GenerateNextTarget();
        UpdateUI();
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
        uiElements.TargetSupertextChange(targetFound);
        //

        if (!gameOver) { 
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
        gameOver = true;
        uiElements.StopTimer();
        uiElements.TargetSupertextChange("");
        uiElements.TargetNameTextChange("FIN DE LA PARTIDA");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void TimerEnded()
    {
        GameOver();
    }
}
