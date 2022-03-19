using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class menuManageButtons : MonoBehaviour
{
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
    }
}
