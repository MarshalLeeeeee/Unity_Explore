using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    public float angularVel;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, angularVel * Time.deltaTime, Space.World);
    }
}
