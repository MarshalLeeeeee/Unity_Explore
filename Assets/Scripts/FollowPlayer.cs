using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance;
    public float forseenAngle;
    ///public Vector3 offset = new Vector3(0, 5, -7);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - cameraDistance * transform.forward;
        transform.eulerAngles = player.transform.eulerAngles + new Vector3(forseenAngle, 0.0f, 0.0f);
    }
}
