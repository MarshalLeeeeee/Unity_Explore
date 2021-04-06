using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    public float shootInterval = 0.5f;
    private bool shootMode = true; // true for semi-auto, false for auto

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CancelInvoke();
            if (shootMode) shootMode = false;
            else shootMode = true;
        }
        if (shootMode)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                InstantiateBullet();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                InvokeRepeating("InstantiateBullet", 0.0f, shootInterval);
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                CancelInvoke();
            }
        }
    }

    void InstantiateBullet()
    {
        Instantiate(bullet, transform);
    }
}
