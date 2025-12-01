using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject howToPlayMenu;

    public static MenuManager Instance;

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

    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("Salir del juego.");

//Directivas (Exclusin de código en tiempo de compilación)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
    Application.Quit();
#endif

    }
}
