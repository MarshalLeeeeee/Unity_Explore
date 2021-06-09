using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShootPos : MonoBehaviour
{
    private Transform anchorTransform;

    private void Start()
    {
        anchorTransform = GameObject.Find("Warrior/Hips/ArmPosition_Right").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchorTransform.position + anchorTransform.up * (-0.9f) + anchorTransform.right * (-0.9f);
    }
}
