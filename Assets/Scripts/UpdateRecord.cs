using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateRecord : MonoBehaviour
{
    public int level;
    private TextMeshProUGUI bestTimeText;

    void Start()
    {
        bestTimeText = GameObject.Find(transform.name + "/Best").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float bestTime = TimeRecord.timeRecord[level];
        if (bestTime < 0.0f) bestTimeText.text = "Best: N / A";
        else
        {
            float currentMinute = (int)(bestTime / 60.0f);
            float currentSecond = bestTime - currentMinute * 60.0f;
            currentSecond = (int)(currentSecond * 100.0f) / 100.0f;
            string timeStr = currentSecond.ToString() + "''";
            if (currentMinute != 0) timeStr = currentMinute.ToString() + "'" + timeStr;
            bestTimeText.text = "Best: " + timeStr;
        }
    }
}
