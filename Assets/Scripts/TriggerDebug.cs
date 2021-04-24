using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDebug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name.ToString() + " triggers with " + other.name.ToString());
    }
}
