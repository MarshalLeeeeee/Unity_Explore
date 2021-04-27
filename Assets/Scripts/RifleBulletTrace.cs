using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletTrace : MonoBehaviour
{
    public float speed = 40.0f;
    public float destroyTime = 1.0f;
    private GetAim getAim;

    private void Start()
    {
        getAim = FindObjectOfType<GetAim>();
        if (getAim.getIsHit())
        {
            Vector3 toDirection = (getAim.getHit().point - transform.position).normalized;
            transform.rotation = transform.rotation * Quaternion.FromToRotation(transform.up, toDirection);
        }        
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.0f);
    }
}
