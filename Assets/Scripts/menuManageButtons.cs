using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class menuManageButtons : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject startScreen;

    private static menuManageButtons instance;
    void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        //startScreen = GameObject.Find("Container");
        //settingsScreen = GameObject.Find("SettingsPanel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); //Main=1
        GameManager.Instance.StartGame();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); // original code to quit Unity player
#endif
    }

    public void GameSettings()
    {
        SceneManager.LoadSceneAsync("UISettings", LoadSceneMode.Additive);
        //startScreen.SetActive(false);
    }
}
