using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public float forwardAcceleration = 1.0f;
    public float forwardSpeedMax = 10.0f;
    public float horizonAcceleration = 0.25f;
    public float horizonSpeedMax = 2.0f;

    private Animator warriorAnim;
    private float forwardSpeed = 0.0f;
    private float horizonSpeed = 0.0f;

    private void Start()
    {
        warriorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizonInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        if (forwardInput != 0.0f) forwardSpeed += Time.deltaTime * forwardAcceleration * forwardInput;
        else
        {
            if (forwardSpeed > 0.0f)
            {
                forwardSpeed -= 0.5f * Time.deltaTime * forwardAcceleration;
                forwardSpeed = forwardSpeed > 0.0f ? forwardSpeed : 0.0f;
            }
            else if (forwardSpeed < 0.0f)
            {
                forwardSpeed += 0.5f * Time.deltaTime * forwardAcceleration;
                forwardSpeed = forwardSpeed < 0.0f ? forwardSpeed : 0.0f;
            }
        }
        forwardSpeed = Mathf.Clamp(forwardSpeed, -horizonSpeedMax, forwardSpeedMax);

        if (horizonInput != 0.0f) horizonSpeed += Time.deltaTime * horizonAcceleration * horizonInput;
        else
        {
            if (horizonSpeed > 0.0f)
            {
                horizonSpeed -= 0.5f * Time.deltaTime * horizonAcceleration;
                horizonSpeed = horizonSpeed > 0.0f ? horizonSpeed : 0.0f;
            }
            else if (horizonSpeed < 0.0f)
            {
                horizonSpeed += 0.5f * Time.deltaTime * horizonAcceleration;
                horizonSpeed = horizonSpeed < 0.0f ? horizonSpeed : 0.0f;
            }
        }
        horizonSpeed = Mathf.Clamp(horizonSpeed, -horizonSpeedMax, horizonSpeedMax);

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizonSpeed);

        warriorAnim.SetFloat("forwardVelocity", forwardSpeed);
        warriorAnim.SetFloat("horizonVelocity", horizonSpeed);
        Debug.Log(forwardSpeed.ToString() + " , " + horizonSpeed.ToString());
    }
}
