using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRunTime : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    public float timer = 0;
    string timeText;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isGameActive)
            {
                timer += Time.deltaTime;

                DisplayTime(timer);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        timerText.text = "Time: " + timeText;
    }
}
