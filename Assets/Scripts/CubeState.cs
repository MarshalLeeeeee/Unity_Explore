using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeState : MonoBehaviour
{
    public TextMeshProUGUI hitText;
    private float health = 100.0f;
    private float power = 0.0f;
    private int clickTime = 0;
    private Coroutine coroutine = null;

    private void Start()
    {
        updateText();
        StartCoroutine(changePosition());
    }

    private void OnMouseDown()
    {
        clickTime += 1;
        health += 2.0f;
        updateText();
        Debug.Log("Click!");
    }

    private void OnMouseUp()
    {
        Debug.Log("Release!");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag!");
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter!");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit!");
    }

    private void OnMouseOver()
    {
        Debug.Log("Tracking!");
    }

    IEnumerator changePosition()
    {
        while (true)
        {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 6.0f), -10);
            health -= 1.0f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void updateText()
    {
        hitText.text = "Hit:" + clickTime;
    }
}
