using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRestrict : MonoBehaviour
{
    public Vector3 restrict;
    public float force;

    private float x;
    private float y;
    private float z;
    private Vector3 origin;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        x = restrict.x;
        y = restrict.y;
        z = restrict.z;
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 relative = transform.position - origin;
        if ((x > 0.0f && Mathf.Abs(relative.x) > x) || (y > 0.0f && Mathf.Abs(relative.y) > y) || (z > 0.0f && Mathf.Abs(relative.z) > z))
        {
            rb.AddForce(-relative.normalized * force, ForceMode.Impulse);
        }
    }
}
