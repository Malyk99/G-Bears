using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartingGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Multiplayer()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Settings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("HomeMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
