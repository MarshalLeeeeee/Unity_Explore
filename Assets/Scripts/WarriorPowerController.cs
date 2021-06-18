using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPowerController : MonoBehaviour
{
    public GameObject powerBar;
    public float powerSpeed = 1.0f;
    private float currentPower = 0.0f;
    private float maxPower = 100.0f;

    // Update is called once per frame
    void Update()
    {
        // nature growth of power
        currentPower += powerSpeed * Time.deltaTime;
        currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);

        // power bar UI
        powerBar.transform.localScale = new Vector3(currentPower / maxPower, 1.0f, 1.0f);
    }

    public void addPower(float p)
    {
        currentPower += p;
        currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
    }
}
