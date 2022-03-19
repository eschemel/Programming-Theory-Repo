using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject startScreen;

    public bool isGameActive { get; set; }
    public bool paused = false;

    //Allows access to the GameManager GameObject between scenes
    private void Awake()
    {
        GameManagerInstance();

        startScreen.SetActive(true);

        //settingsScreen.SetActive(false);
        //Not Paused
        paused = false;
        pauseScreen.SetActive(false);
    }

    void GameManagerInstance()
    {
        //Ensure only a single instance exists
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        //settingsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause - Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
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
        //gameOverText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);

        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    /*public void GameSettings()
    {
        settingsScreen.SetActive(true);
        if(!isGameActive)
        {
            startScreen.SetActive(false);
        } else if (paused)
        {
            pauseScreen.SetActive(false);
        }
    }*/

    /*public void SettingsBacktoMenu() 
    {
        settingsScreen.SetActive(false);
        if (!isGameActive)
        {
            startScreen.SetActive(true);
        }
        else if (paused)
        {
            pauseScreen.SetActive(true);
        }
    }*/

    public void QuitToMenu()
    {
        isGameActive = false;
        paused = false;
        SceneManager.LoadScene(0); //Menu=0
        pauseScreen.SetActive(false);
    }
}