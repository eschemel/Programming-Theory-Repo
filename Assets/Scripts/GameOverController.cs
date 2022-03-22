using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;

    public GameObject bestTimePanel;
    public GameObject inputFieldPanel;

    public TextMeshProUGUI bestScoreText;
    public TMP_InputField m_inputField;

    public string m_cPlayerName;

    private void Awake()
    {
        instance = this; // this, is a special C# keyword that means “the object that currently runs that function” (Singleton)
    }

    // Start is called before the first frame update
    void Start()
    {
        inputFieldPanel.SetActive(true);
        bestTimePanel.SetActive(false);
    }

    public void InputPlayerName() //Save Player Name
    {
        m_inputField = GameObject.Find("PlayerNameInputField").GetComponent<TMP_InputField>();

        m_cPlayerName = m_inputField.text;
        Debug.Log(m_cPlayerName);
    }

    public string PlayerName()
    {
        return m_cPlayerName;
    }

    public void DisplayBestTime()
    {
        if (bestTimePanel.activeSelf != true)
        {
            bestTimePanel.SetActive(true);
        }
        
        bestScoreText.text = TheBest.instance.displayText;
    }

    public void SaveButton()
    {
        TheBest.instance.HighScore();

        if (inputFieldPanel.activeSelf != false)
        {
            inputFieldPanel.SetActive(false);
        }
    }
}
