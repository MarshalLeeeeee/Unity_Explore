using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public int level;
    public GameObject panel;
    public static bool isPause = false;
    private Button resumeButton;
    private Color highlightColor = new Color(245.0f/255.0f,245.0f/255.0f,245.0f/255.0f, 100.0f/255.0f);
    private TextMeshProUGUI pauseTitle;
    private TimeConut tc;

    private void Start()
    {
        resumeButton = panel.transform.Find("ResumeButton").gameObject.GetComponent<Button>();
        pauseTitle = panel.transform.Find("PauseText").gameObject.GetComponent<TextMeshProUGUI>();
        tc = gameObject.GetComponent<TimeConut>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause && WarriorHealthController.alive) resume();
            else pause();
        }

        if (!WarriorHealthController.alive)
        {
            if (!isPause) pause();
            pauseTitle.text = "Died";
            ColorBlock colorVar = resumeButton.colors;
            colorVar.highlightedColor = highlightColor;
            resumeButton.colors = colorVar;
        }

        if (LevelEnd.levelEnd)
        {
            if (!isPause) pause();
            float finalTime = tc.getTime();
            TimeRecord.changeRecord(level, finalTime);
            float currentMinute = (int)(finalTime / 60.0f);
            float currentSecond = finalTime - currentMinute * 60.0f;
            currentSecond = (int)(currentSecond * 100.0f) / 100.0f;
            string timeStr = currentSecond.ToString() + "''";
            if (currentMinute != 0) timeStr = currentMinute.ToString() + "'" + timeStr;
            pauseTitle.text = "Win time " + timeStr;
            ColorBlock colorVar = resumeButton.colors;
            colorVar.highlightedColor = highlightColor;
            resumeButton.colors = colorVar;
        }
    }

    private void OnEnable()
    {
        isPause = false;
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }

    public void pause()
    {
        isPause = true;
        Time.timeScale = 0.0f;
        panel.SetActive(true);
    }

    public void resume()
    {
        if (WarriorHealthController.alive)
        {
            isPause = false;
            Time.timeScale = 1.0f;
            panel.SetActive(false);
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        SceneManager.LoadScene(1);
    }
}
