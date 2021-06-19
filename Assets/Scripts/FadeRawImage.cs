using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeRawImage : MonoBehaviour
{
    public float aMax;
    public float aMin;
    public float speed;
    private  RawImage img;
    private bool decrease = true;
    private float r;
    private float g;
    private float b;
    private float a;

    private void Start()
    {
        img = gameObject.GetComponent<RawImage>();
        r = img.color.r;
        g = img.color.g;
        b = img.color.b;
        a = img.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (decrease)
        {
            a -= speed * Time.deltaTime;
            if (a < aMin) { decrease = false; a = aMin; }
        }
        else
        {
            a += speed * Time.deltaTime;
            if (a > aMax) { decrease = true; a = aMax; }
        }
        img.color = new Color(r, g, b, a);
    }
}
