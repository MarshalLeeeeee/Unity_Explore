using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject panel;
    public static bool isPause = false;
    private Button resumeButton;
    private Color highlightColor = new Color(245.0f/255.0f,245.0f/255.0f,245.0f/255.0f, 100.0f/255.0f);
    private TextMeshProUGUI pauseTitle;

    private void Start()
    {
        resumeButton = panel.transform.Find("ResumeButton").gameObject.GetComponent<Button>();
        pauseTitle = panel.transform.Find("PauseText").gameObject.GetComponent<TextMeshProUGUI>();
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
