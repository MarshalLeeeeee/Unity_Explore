using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncollideFeedback : MonoBehaviour
{
    public float collisionForce = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            Debug.Log(collision.GetContact(0).normal.ToString());
            collision.rigidbody.AddForce(-collision.GetContact(0).normal * collisionForce, ForceMode.Acceleration);
        }
    }
}
