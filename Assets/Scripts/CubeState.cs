using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CubeState : MonoBehaviour
{
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI healthText;
    public Button restartButton;
    private int health = 100;
    private int power = 0;
    private int clickTime = 0;
    private Coroutine coroutine = null;
    private bool over = false;

    private void Start()
    {
        restartButton.gameObject.SetActive(false);
        updateHitText();
        updateHealthText();
        StartCoroutine(changePosition());
    }

    private void Update()
    {
        if (over) return;
        updateHitText();
        updateHealthText();
        if (health <= 0)
        {
            gameOver();
        }
    }

    private void OnMouseDown()
    {
        clickTime += 1;
        health += 2;
        Debug.Log("Click!");
    }

    private void OnMouseUp()
    {
        if (over) return;
        Debug.Log("Release!");
    }

    private void OnMouseDrag()
    {
        if (over) return;
        Debug.Log("Drag!");
    }

    private void OnMouseEnter()
    {
        if (over) return;
        Debug.Log("Enter!");
    }

    private void OnMouseExit()
    {
        if (over) return;
        Debug.Log("Exit!");
    }

    private void OnMouseOver()
    {
        if (over) return;
        Debug.Log("Tracking!");
    }

    IEnumerator changePosition()
    {
        while (true && !over)
        {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 6.0f), -10);
            health -= 1;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void updateHitText()
    {
        hitText.text = "Hit:" + clickTime;
    }

    void updateHealthText()
    {
        healthText.text = "Health:" + health;
    }

    void gameOver()
    {
        over = true;
        restartButton.gameObject.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
