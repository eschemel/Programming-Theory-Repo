using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO; //required for JsonUtility

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ENCAPSULATION
    public bool isGameActive { get; set; }
    public bool paused = false;

    //Allows access to the GameManager GameObject between scenes
    private void Awake()
    {
        GameManagerInstance();

        //Not Paused
        paused = false;
    }

    private void GameManagerInstance()
    {
        // ABSTRACTION
        //Ensure only a single instance exists
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        // Pause - Check if the user has pressed the Esc key
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        //Time.timeScale = 0;
        isGameActive = false;
        StartCoroutine("FadeToGameOver");
    }

    private IEnumerator FadeToGameOver()
    {
        //ignoring Time.timeScale = 0
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadSceneAsync("UIGameOver", LoadSceneMode.Additive);
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(1); //Main=1
        paused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void PauseGame()
    {
        // ABSTRACTION
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            AudioListener.pause = true;
            SceneManager.LoadSceneAsync("UIPause", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("UIPause");
            paused = false;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    public void PauseQuitToMenu()
    {
        isGameActive = false;
        paused = false;
        SceneManager.UnloadSceneAsync("UIPause");
        SceneManager.LoadScene(0); //Menu=0
        AudioListener.pause = false;
    }

    public void GameOverQuitToMenu()
    {
        isGameActive = false;
        paused = false;
        SceneManager.UnloadSceneAsync("UIGameOver");
        SceneManager.LoadScene(0); //Menu=0
        AudioListener.pause = false;
    }
}