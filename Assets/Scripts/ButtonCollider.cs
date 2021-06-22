using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonCollider : MonoBehaviour
{
    public Color enterColor;
    private TextMeshProUGUI bestTimeText;
    private Color initColor;

    void Start()
    {
        bestTimeText = GameObject.Find(transform.name + "/Best").GetComponent<TextMeshProUGUI>();
        initColor = bestTimeText.color;
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
