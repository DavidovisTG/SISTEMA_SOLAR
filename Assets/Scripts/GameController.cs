using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private List<string> targetList = new List<string>() { "Jupiter", "" };

    private string targetToFind;
    private int lives = 3;

    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateNextTarget();
        UpdateUI();
    }

    void UpdateUI()
    {
        targetNameText.text = "Busca... " + targetToFind;
        livesText.text = "Vidas: " + lives;
        scoreText.text = "Puntuación: " + score;
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
        targetNameText.text = "GAME OVER";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
