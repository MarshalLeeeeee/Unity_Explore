using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForward : MonoBehaviour
{
    public float speed = 40.0f;
    public float destroyTime = 1.0f;

    private void Start()
    {
        Debug.Log("Bullet out " + transform.position);
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
