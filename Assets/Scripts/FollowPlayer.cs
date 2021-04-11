using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float cameraForwardDistance;
    public float cameraUpDistance;
    public float forseenAngle;
    public bool mouseControl = true;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position + cameraForwardDistance * transform.forward + cameraUpDistance * transform.up;
        if (mouseControl)
        {
            xAngle += Input.GetAxis("Mouse Y");
            yAngle -= Input.GetAxis("Mouse X");
        }
        transform.Rotate(xAngle + forseenAngle, yAngle, 0.0f);
    }
}
