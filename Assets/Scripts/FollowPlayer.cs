using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float cameraForwardDistance;
    public float cameraUpDistance;
    public float forseenAngle;
    public float verticalSensitivity = 0.5f;
    public float recoilAngle = 10.0f;
    private float xAngle;

    private void Start()
    {
        xAngle = forseenAngle;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.Find("Hips/Spine/Chest/Neck/Head").position;
        xAngle -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        xAngle = Mathf.Clamp(xAngle, -90.0f, 90.0f);
        transform.Rotate(xAngle, 0.0f, 0.0f);
        transform.position = transform.position + cameraForwardDistance * transform.forward + cameraUpDistance * transform.up;
    }

    public float getPoseXAngle()
    {
        return xAngle - forseenAngle;
    }

    public void recoil()
    {
        xAngle -= recoilAngle;
        xAngle = Mathf.Clamp(xAngle, -90.0f, 90.0f);
    }
}
