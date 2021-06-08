using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonCollider : MonoBehaviour
{
    private TextMeshProUGUI bestTimeText;
    private Color initColor;
    private Color enterColor;

    void Start()
    {
        bestTimeText = GameObject.Find(transform.name + "/Best").GetComponent<TextMeshProUGUI>();
        initColor = bestTimeText.color;
        enterColor = new Color(255.0f, 255.0f, 0.0f); ;
    }

    private void OnMouseEnter()
    {
        Debug.Log(bestTimeText.color);
    }

    public void pointerEnter()
    {
        bestTimeText.color = enterColor;
    }

    public void pointerExit()
    {
        bestTimeText.color = initColor;
    }
}
