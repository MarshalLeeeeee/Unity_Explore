using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Through : MonoBehaviour
{
    public float throughtForce = 100.0f;

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.attachedRigidbody && other.tag != "Bullet" && !other.isTrigger)
        {
            other.isTrigger = true;
            if (Vector3.Dot(other.transform.position - transform.position, transform.up) > 0.0f)
            {
                other.attachedRigidbody.AddForce(-transform.up * throughtForce, ForceMode.Impulse);
            }
            else
            {
                other.attachedRigidbody.AddForce(transform.up * throughtForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.tag != "Bullet" && other.isTrigger) other.isTrigger = false;
    }
}
