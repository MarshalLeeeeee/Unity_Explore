using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float upVelocity = 10.0f;
    private Vector3 velocityLocal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (Vector3.Dot(other.transform.position - transform.position, transform.up) > 0.0f)
            {
                velocityLocal = Matrix4x4.Rotate(Quaternion.FromToRotation(transform.up, Vector3.up)).MultiplyPoint3x4(other.attachedRigidbody.velocity);
                velocityLocal = new Vector3(velocityLocal.x, Mathf.Clamp(velocityLocal.y, 0.0f, Mathf.Infinity), velocityLocal.z);
                other.attachedRigidbody.velocity = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, transform.up)).MultiplyPoint3x4(velocityLocal);
                other.attachedRigidbody.AddForce(transform.up * upVelocity, ForceMode.Impulse);
            }
            else
            {
                velocityLocal = Matrix4x4.Rotate(Quaternion.FromToRotation(transform.up, Vector3.up)).MultiplyPoint3x4(other.attachedRigidbody.velocity);
                velocityLocal = new Vector3(velocityLocal.x, Mathf.Clamp(velocityLocal.y, -Mathf.Infinity, 0.0f), velocityLocal.z);
                other.attachedRigidbody.velocity = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, transform.up)).MultiplyPoint3x4(velocityLocal);
                other.attachedRigidbody.AddForce(-transform.up * upVelocity, ForceMode.Impulse);
            }
        }
    }
}
