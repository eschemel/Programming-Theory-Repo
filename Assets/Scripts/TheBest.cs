using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; //required for JsonUtility
//using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TheBest : MonoBehaviour
{
    public static TheBest instance;

    [Header("Save Data")]
    public string m_playerName;
    // for times: lower is better
    public float m_bestTime;
    private bool _reset = false;
    private string timeText;

    public string displayText;
    public string displayTextBest;

    [Header("Current Game")]
    public float m_cBestTime; //current game time

    private bool HaveBestTime;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadBestTime();
        if (m_bestTime > 0)
        {
            HaveBestTime = true;
        }
        Debug.Log("HaveBestTime: " + HaveBestTime);
    }

    public void HighScore() //RecordTimeIfLower
    {
        if (!_reset)
        {
            UIRunTime.instance.EndTime();

            m_cBestTime = UIRunTime.instance.timer;
            Debug.Log("New Time: " + m_cBestTime);

            //First check for IF new high score
            if (!HaveBestTime || m_cBestTime < m_bestTime || m_playerName == "None")
            {
                GameOverController.instance.PlayerName();
                m_bestTime = m_cBestTime;
                m_playerName = GameOverController.instance.m_cPlayerName;
                Debug.Log("m_bestTime: " + m_bestTime + ", m_playerName: " + m_playerName);
                
                DisplayTime(m_bestTime);
                displayText = "NEW Best Time- Player: " + m_playerName + ", Time: " + timeText;
                GameOverController.instance.DisplayBestTime();

                SaveBestTime();
            }
            else
            {
                //Don't Save game data, go straigh back to menu
                LoadBestTime();
                
                displayText = displayTextBest;
                GameOverController.instance.DisplayBestTime();

                StartCoroutine("ExitToMainMenu");
            }
        }
        else
        {
            m_cBestTime = 0f;
            m_playerName = "None";
            displayText = "Best Time- Player: NAN, Time: 00:00:00";
            //bestScoreText.text = displayText;
            GameOverController.instance.DisplayBestTime();

            SaveBestTime();
        }
    }

    public string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        Debug.Log(timeText);
        return timeText;
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
        Debug.Log("Saving start");
        SaveData data = new SaveData();

        data.m_playerName = m_playerName;
        data.m_bestTime = m_bestTime;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Json: " + json);

        StartCoroutine("ExitToMainMenu");
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

            DisplayTime(m_bestTime);

            displayTextBest = "Best Time- Player: " + m_playerName + ", Time: " + timeText;
        }
    }

    private IEnumerator ExitToMainMenu()
    {
        //wait ignoring Time.timeScale = 0
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        Debug.Log("Exit!");
        //Reset///////////////////////////////////////////
        /*LoadBestTime();
        if (m_bestTime > 0)
        {
            HaveBestTime = true;
        }
        Debug.Log("HaveBestTime: " + HaveBestTime);*/
        //Reset///////////////////////////////////////////

        SceneManager.UnloadSceneAsync("UIGameOver");
        SceneManager.LoadScene(0); //Menu=0
    }
}