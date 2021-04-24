using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    // Print when Collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(transform.name.ToString() + " collides with " + collision.transform.name.ToString());
    }
}
