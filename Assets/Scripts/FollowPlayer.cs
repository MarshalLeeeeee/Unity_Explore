using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance;
    public float forseenAngle;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - cameraDistance * transform.forward;
        transform.rotation = player.transform.rotation;
        xAngle += Input.GetAxis("Mouse Y");
        yAngle -= Input.GetAxis("Mouse X");
        transform.Rotate(xAngle + forseenAngle, yAngle, 0.0f);
    }
}
