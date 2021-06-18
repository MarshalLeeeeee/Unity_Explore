using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseWarrior : MonoBehaviour
{
    private FaceWarrior fw;

    private void Start()
    {
        fw = transform.parent.gameObject.GetComponent<FaceWarrior>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            fw.ifSense(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            fw.ifSense(false);
        }
    }
}
