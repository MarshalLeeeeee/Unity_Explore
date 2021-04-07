using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControllerCoroutine : MonoBehaviour
{
    public GameObject bullet;
    public float shootInterval = 0.5f;
    private bool shootMode = true; // true for semi-auto, false for auto
    private float prevTime = -1.0f;
    private Coroutine coroutine = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (coroutine != null) StopCoroutine(coroutine);
            if (shootMode) shootMode = false;
            else shootMode = true;
        }
        if (shootMode)
        {
            if (Input.GetMouseButtonDown(0) && (Time.time >= prevTime + shootInterval))
            {
                Instantiate(bullet, transform);
                prevTime = Time.time;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                coroutine = StartCoroutine(InstantiateBulletAutoRoutine());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine(coroutine);
                coroutine = null;
                prevTime = Time.time;
            }
        }
    }

    IEnumerator InstantiateBulletAutoRoutine()
    {
        while (true)
        {
            Instantiate(bullet, transform);
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
