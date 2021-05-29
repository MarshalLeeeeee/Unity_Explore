using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger"+other.transform.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider" + collision.transform.name);
    }
}
