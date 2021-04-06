using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLog : MonoBehaviour
{
    public string log;

    // Update is called once per frame
    void Update()
    {
        print(log);
    }
}
