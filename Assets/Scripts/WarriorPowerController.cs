using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPowerController : MonoBehaviour
{
    public float powerSpeed = 1.0f;
    private float currentPower = 0.0f;
    private float maxPower = 100.0f;

    // Update is called once per frame
    void Update()
    {
        currentPower += powerSpeed * Time.deltaTime;
        currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
    }

    public void addPower(float p)
    {
        currentPower += p;
        currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
    }
}
