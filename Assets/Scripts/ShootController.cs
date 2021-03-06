using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    public float shootInterval = 0.5f;
    private bool shootMode = true; // true for semi-auto, false for auto
    private float prevTime = -1.0f;
    private bool inSemiShoot = false;

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
            if (Input.GetMouseButtonDown(0) && (Time.time >= prevTime + shootInterval))
            {
                Instantiate(bullet, transform);
                prevTime = Time.time;
                inSemiShoot = true;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                inSemiShoot = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && (Time.time >= prevTime + shootInterval) && !inSemiShoot)
            {
                Instantiate(bullet, transform);
                prevTime = Time.time;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                inSemiShoot = false;
            }
        }
    }
}
