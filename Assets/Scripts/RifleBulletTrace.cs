using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletTrace : MonoBehaviour
{
    public float impluse = 100.0f;
    public float maxDestroyTime = 10.0f;
    private GetAim getAim;
    private Vector3 hitPoint;
    private bool hasAim = false;
    private string shooterTag;
    private Rigidbody rb;

    private void Start()
    {
        shooterTag = transform.parent.tag;
        getAim = FindObjectOfType<GetAim>();
        if (getAim.getIsHit())
        {
            hasAim = true;
            hitPoint = getAim.getHit().point;
            Vector3 toDirection = (hitPoint - transform.position).normalized;
            transform.rotation = transform.rotation * Quaternion.FromToRotation(transform.up, toDirection);
        }
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * impluse, ForceMode.Impulse);
        Destroy(gameObject, maxDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasAim)
        {
            if ((transform.position - hitPoint).magnitude < 1.2f)
            {
                Destroy(gameObject, 0.0f);
            }
        }
    }
}
