using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeConut : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public bool ifCount;
    private float currentTime;
    private int currentMinute = 0;
    private float currentSecond = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (ifCount)
        {
            currentTime = Time.time;
            currentMinute = (int)(currentTime / 60.0f);
            currentSecond = currentTime - currentMinute * 60.0f;
            currentSecond = (int)(currentSecond * 100.0f) / 100.0f;
            string timeStr = currentSecond.ToString() + "''";
            if (currentMinute != 0) timeStr = currentMinute.ToString() + "'" + timeStr;
            timeText.text = timeStr;
        }
        else timeText.text = "N / A";
    }
}
