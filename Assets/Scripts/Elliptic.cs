using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elliptic : MonoBehaviour
{
    private float m_time = 0.0f;
    public float rx;
    public float rz;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(rx * Mathf.Cos(2.0f * Mathf.PI * m_time), 0, rz * Mathf.Sin(2.0f * Mathf.PI * m_time));
        m_time += Time.deltaTime;
    }
}
