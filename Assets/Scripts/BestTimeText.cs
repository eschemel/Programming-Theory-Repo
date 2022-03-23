using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestTimeText : MonoBehaviour
{
    public TextMeshProUGUI bestScoreTextStart;
    private string timeText;

    private void Start()
    {
        TheBest.instance.LoadBestTime();

        DisplayTime(TheBest.instance.m_bestTime);

        bestScoreTextStart.text = "Best Time- Player: " + TheBest.instance.m_playerName + ", Time: " + timeText;

    }

    private string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        Debug.Log(timeText);
        return timeText;
    }
}
