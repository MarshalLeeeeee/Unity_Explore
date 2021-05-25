using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpForce : MonoBehaviour
{
    public float upVelocity = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.velocity.y <= 0.0f) 
                other.attachedRigidbody.velocity = new Vector3(other.attachedRigidbody.velocity.x, 0.0f, other.attachedRigidbody.velocity.z);
            other.attachedRigidbody.AddForce(Vector3.up * upVelocity, ForceMode.VelocityChange);
        }
    }
}
