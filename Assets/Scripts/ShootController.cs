using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    public int shootSpeed = 10;
    private bool shootMode = true; // true for semi-auto, false for auto
    private int previousFrame = -999;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (shootMode) shootMode = false;
            else shootMode = true;
        }
        if (shootMode)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                && Time.frameCount - previousFrame > shootSpeed)
            {
                Instantiate(bullet, transform);
                previousFrame = Time.frameCount;
            }
        }
        else
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                && Time.frameCount - previousFrame > shootSpeed)
            {
                Instantiate(bullet, transform);
                previousFrame = Time.frameCount;
            }
        }
    }
}
