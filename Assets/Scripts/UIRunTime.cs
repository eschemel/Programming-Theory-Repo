using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRunTime : MonoBehaviour
{
    public static UIRunTime instance;

    public TextMeshProUGUI timerText;

    public float timer;
    private string timeText;

    private void Awake()
    {
        instance = this; // this, is a special C# keyword that means “the object that currently runs that function” (Singleton)
    }

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            //if game is active then update timer
            if (GameManager.Instance.isGameActive)
            {
                timer += Time.deltaTime;

                DisplayTime(timer);
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        timerText.text = "Time: " + timeText;
    }

    public float EndTime()
    {
        return timer;
    }
}
