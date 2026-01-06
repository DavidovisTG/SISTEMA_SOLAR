using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //CONVENIENCE VARIABLES
    UIDynamicElements uiElements;

    //
    private List<string> presetTargetList = new List<string>() { "Sol", "Mercurio", "Venus", "Tierra", "Luna", "Marte", "Júpiter", "Saturno", "Urano", "Neptuno", "Pluto" };
    private List<string> inGameTargetList = new List<string>();

    [SerializeField] private string targetToFind;
    [SerializeField] private int lives = 3;
    [SerializeField] private float timerTimeInSeconds = 60;
    [SerializeField] private int targetDefaultValue = 1000;
    [SerializeField] private int targetCurrentValue;
    [SerializeField] private int score = 0;

    [SerializeField] Timer timer;

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
        //Debug.LogError("PASA POR AQUI");
        uiElements = FindFirstObjectByType<UIDynamicElements>();
        timer = uiElements.GetComponentInChildren<Timer>();

        StartCoroutine(BeginGame());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void Update()
    {
        //UpdateUI();
    }
    IEnumerator BeginGame()
    {
        ResetEverything();
        yield return uiElements.StartAnimation();
        uiElements.AllUIVisible(true);

        timer.SetSeconds(timerTimeInSeconds);
        timer.Run();
        timer.SetActive(true);

        GenerateNextTarget();
        UpdateUI();
    }

    void UpdateUI()
    {
        uiElements.TargetNameTextChange("" + targetToFind);
        uiElements.LivesNumberTextChange("" + lives);
        uiElements.ScoreNumberTextChange("" + score.ToString("00000"));
    }


    public void OnImageTargetFound(string targetFound)
    {
        //DEBUGGING
        //uiElements.TargetSupertextChange(targetFound);
        //

        if (!gameOver)
        {
            if (targetFound.Equals(targetToFind))
            {
                //Correct answer
                score += targetCurrentValue;
                GenerateNextTarget();
            }
            else
            {
                //Incorrect answer
                lives--;
                if (lives <= 0)
                {
                    GameOverByLives();
                }
            }
        }

        UpdateUI();
    }

    void GenerateNextTarget()
    {
        targetCurrentValue = targetDefaultValue;

        if (inGameTargetList.Count <= 0)
        {
            //WIN SEQUENCE
            WinSequence();
        }
        else
        {
            int randomPosition = Random.Range(0, inGameTargetList.Count);
            targetToFind = inGameTargetList[randomPosition];
            inGameTargetList.RemoveAt(randomPosition);
        }
    }

    void GameOverByLives()
    {
        gameOver = true;
        timer.Stop(Color.red);
        uiElements.TargetSupertextChange("Has perdido tus vidas.");
        uiElements.TargetNameTextChange("FIN DE LA PARTIDA");
        GenerateFinalScore(false);
    }

    void GameOverByTime()
    {
        gameOver = true;
        timer.Stop(Color.red);
        uiElements.TargetSupertextChange("Se ha acabado el tiempo.");
        uiElements.TargetNameTextChange("FIN DE LA PARTIDA");
        GenerateFinalScore(false);
    }

    void WinSequence()
    {
        uiElements.DynamicBoxVisible(true);
        gameOver = true;
        timer.Stop(Color.green);
        uiElements.TargetSupertextChange("¡Has escaneado todo!");
        uiElements.TargetNameTextChange("FIN DE LA PARTIDA");
        GenerateFinalScore(true);
    }

    void ResetEverything()
    {
        uiElements.AllUIVisible(false);
        uiElements.SetDynBoxBGColor(new Color(0, 0, 0, 0));
        timer.SetSeconds(timerTimeInSeconds);
        inGameTargetList.Clear();
        inGameTargetList.AddRange(presetTargetList);
        lives = 3;
        score = 0;
    }

    void GenerateFinalScore(bool win)
    {
        uiElements.DynamicBoxVisible(true);
        uiElements.SetDynBoxBGColor(new Color(0,0,0,(100/255F))); //DE SISTEMA 0-255 A 0.0-1.0
        if (win)
        {
            int livesBonus = lives * 250;
            int timeBonus = Mathf.RoundToInt(timer.GetTimeLeft() * 50);
            int finalScore = score + livesBonus + timeBonus;
            uiElements.DynamicTextChange(65,
                "Puntos obtenidos: " + score + "\n" +
                "Bonus por vidas: (" + lives + "x 250) = " + livesBonus + "\n" +
                "Bonus por tiempo: (" + timer.GetTimeLeft().ToString("00.00", CultureInfo.InvariantCulture) + "x 50) = " + timeBonus + "\n" +
                "PUNTUACION FINAL: " + finalScore
                );
        } else
        {
            uiElements.DynamicTextChange(65,
                "Puntos obtenidos: " + score + "\n" +
                "[Sin bonificaciones]\n" +
                "PUNTUACION FINAL: " + score
                );
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void TimerEnded()
    {
        GameOverByTime();
    }
}
