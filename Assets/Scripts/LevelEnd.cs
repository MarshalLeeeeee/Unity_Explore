using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public static bool levelEnd;

    private void OnEnable()
    {
        levelEnd = false;
    }

    private void OnDisable()
    {
        levelEnd = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            levelEnd = true;
        }
    }
}
