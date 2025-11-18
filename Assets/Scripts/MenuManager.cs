using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void QuitGame()
    {
        Debug.Log("Salir del juego.");
        Application.Quit(); //Cierra el programa en Android
    }
}
