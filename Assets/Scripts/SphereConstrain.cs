using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereConstrain : MonoBehaviour
{
    public float radius;
    public float force;
    private Vector3 origin;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 relative = origin - transform.position;
        if (relative.magnitude > radius)
        {
            rb.AddForce(relative*force, ForceMode.Force);
        }
    }
}
