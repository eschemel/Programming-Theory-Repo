using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    //Open Settings Menu
    public void GameSettings()
    {
        SceneManager.LoadSceneAsync("UISettings", LoadSceneMode.Additive);
    }

    //Restart Level
    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    //Quit to Main Menu
    public void QuitToMainMenu()
    {
        GameManager.Instance.QuitToMenu();
    }

    //Close Pause window / Resume game
    public void ClosePause()
    {
        GameManager.Instance.PauseGame();
    }
}
