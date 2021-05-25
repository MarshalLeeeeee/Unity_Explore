using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAim : MonoBehaviour
{
    private RaycastHit hit;
    private bool isHit;
    private float maxDistance;

    private void Start()
    {
        maxDistance = GetComponent<Camera>().farClipPlane;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(new Ray(transform.position + transform.forward * 2.0f, transform.forward), out hit, maxDistance)) isHit = true;
            else isHit = false;
        }
    }

    public bool getIsHit()
    {
        return isHit;
    }

    public RaycastHit getHit()
    {
        return hit;
    }
}
