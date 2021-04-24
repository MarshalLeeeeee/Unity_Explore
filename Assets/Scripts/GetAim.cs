using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAim : MonoBehaviour
{
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit))
            {
                //Debug.Log(hit.point.ToString());
                ;
            }
        }
    }
}
