using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO; //required for JsonUtility

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject startScreen;
    public GameObject gameOverPanel;

    public bool isGameActive { get; set; }
    public bool paused = false;

    [Header("Save Data")]
    public string m_playerName;
    public float m_bestTime;
    private bool _reset = false;

    [Header("Current Game")]
    public string m_cPlayerName; //Hidden until high score.
    public float m_cBestTime; //current game time

    //Allows access to the GameManager GameObject between scenes
    private void Awake()
    {
        GameManagerInstance();

        startScreen.gameObject.SetActive(true);

        //Not Paused
        paused = false;

        gameOverPanel.gameObject.SetActive(false);
    }

    private void GameManagerInstance()
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
    private void Start()
    {

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
        startScreen.gameObject.SetActive(false);
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        //Time.timeScale = 0;
        isGameActive = false;
        HighScore();
        StartCoroutine("FadeToGameOver");
    }

    private IEnumerator FadeToGameOver()
    {
        yield return new WaitForSeconds(3);
        gameOverPanel.gameObject.SetActive(true);
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

    public void QuitToMenu()
    {
        isGameActive = false;
        paused = false;
        SceneManager.LoadScene(0); //Menu=0
        AudioListener.pause = false;
    }

    private void HighScore()
    {
        if (!_reset)
        {
            //current game - if new best time
            if (m_cBestTime > m_bestTime)
            {
                m_bestTime = m_cBestTime;
                m_playerName = m_cPlayerName;
                //bestScoreText.text = "High Score : " + m_playerName + " : " + m_HighPoints;

                SaveBestTime();
            }
        }
        else
        {
            m_cBestTime = 0f;
            m_playerName = m_cPlayerName;
            //bestScoreText.text = "High Score : " + m_playerName + " : " + m_HighPoints;

            SaveBestTime();
        }
    }

    ////////////SAVE DATA////////////
    [System.Serializable] //required for JsonUtility
    class SaveData
    {
        public string m_playerName;
        public float m_bestTime;
    }

    //Saving to Json
    public void SaveBestTime()
    {
        SaveData data = new SaveData();

        data.m_playerName = m_playerName;
        data.m_bestTime = m_bestTime;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //Loading from Json
    public void LoadBestTime()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_playerName = data.m_playerName;
            m_bestTime = data.m_bestTime;
        }
    }
}