using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject howToPlayMenu;

    public static MenuManager Instance;

    private void Awake()
    {
        
    }

    public void MainMenuScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PlayGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("PLAY GAME");
    }

    public void HowToPlayScreen()
    {
        Debug.Log("HOW TO PLAY GAME");
        howToPlayMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ExitHTPScreen()
    {
        Debug.Log("EXIT HTP SCREEN");
        howToPlayMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("Salir del juego.");

//Directivas (Exclusión de código en tiempo de compilación)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
    Application.Quit();
#endif

    }
}
