using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float impluse = 100.0f;
    public float maxDestroyTime = 10.0f;
    public GameObject bouncePad;
    public GameObject throughPad;
    public GameObject bulletHit;

    private Transform shooter;
    private Rigidbody rb;
    private bool bounceTrigger = false;
    private bool throughTrigger = false;
    private GameObject trial;

    private void Start()
    {
        transform.Rotate(90.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * impluse, ForceMode.Impulse);
        Destroy(gameObject, maxDestroyTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((bounceTrigger || throughTrigger) && collision.transform.tag == "Floor")
        {
            Transform colliderTransform = collision.transform;
            Vector3 projectPoint = Vector3.ProjectOnPlane((collision.GetContact(0).point - colliderTransform.position), colliderTransform.up) + colliderTransform.position;
            if (bounceTrigger)
            {
                Instantiate(bouncePad, projectPoint, Quaternion.FromToRotation(colliderTransform.up, Vector3.up));
            }
            else if (throughTrigger)
            {
                Instantiate(throughPad, projectPoint, Quaternion.FromToRotation(colliderTransform.up, Vector3.up));
            }
        }
        Destroy(gameObject, 1.0f);
        if (trial) Destroy(trial);
        Instantiate(bulletHit, collision.GetContact(0).point, Quaternion.identity);
        collision.GetContact(0).thisCollider.isTrigger = true;
    }

    public void setShooter(Transform transform)
    {
        shooter = transform;
    }

    public void setBounce()
    {
        bounceTrigger = true;
    }

    public void setThrough()
    {
        throughTrigger = true;
    }

    public void setTrial(GameObject t)
    {
        trial = t;
    }
}
