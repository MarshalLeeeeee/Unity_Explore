using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncollideFeedback : MonoBehaviour
{
    public float collisionForce = 10.0f;
    public float verticalRatio = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            Vector3 force = -collision.GetContact(0).normal * collisionForce;
            force = new Vector3(force.x, force.y * verticalRatio, force.z);
            collision.rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
