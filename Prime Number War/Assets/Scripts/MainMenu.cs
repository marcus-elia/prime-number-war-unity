using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play1()
    {
        ScoreManager.gameMode = GameMode.Alone;
        SceneManager.LoadScene("Single");
    }

    public void Play2()
    {
        ScoreManager.gameMode = GameMode.VersusComputer;
        SceneManager.LoadScene("VersusComputer");
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
