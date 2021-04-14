using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public float forwardAcceleration = 1.0f;
    public float forwardSpeedMax = 10.0f;
    public float horizonAcceleration = 0.25f;
    public float horizonSpeedMax = 2.0f;
    public float upForce = 5.0f;
    public float riseTime = 0.15f;
    public float horizonSensitivity = 0.5f;
    public float verticalSensitivity = 0.5f;
    public float surfMinTime = 0.5f;

    private Animator warriorAnim;
    private Rigidbody warriorRb;
    private float forwardSpeed = 0.0f;
    private float horizonSpeed = 0.0f;
    private float yAngle = 0.0f;
    private bool onGround = true;
    private bool inAir = false;
    private bool jumpTrigger = false;
    private bool surfTrigger = false;
    private float surfStart;

    private void Start()
    {
        warriorAnim = GetComponent<Animator>();
        warriorRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (jumpTrigger)
        {
            warriorRb.AddForce(upForce * transform.up, ForceMode.Acceleration);
            jumpTrigger = false;
            inAir = true;
        }
    }

    private void Update()
    {
        if (surfTrigger && Time.time > surfStart + surfMinTime)
        {
            warriorAnim.SetTrigger("surfTrigger");
            inAir = true;
            surfTrigger = false;
        }

        yAngle = Input.GetAxis("Mouse X") * verticalSensitivity;
        transform.Rotate(0.0f, yAngle, 0.0f);

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

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jumpTrigger = true;
            warriorAnim.SetTrigger("jumpTrigger");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") && collision.contacts[0].normal.normalized == Vector3.up)
        {
            if (!onGround)
            {
                if (inAir)
                {
                    warriorAnim.SetTrigger("landTrigger");
                    inAir = false;
                }
                onGround = true;
            }
            if (surfTrigger)
            {
                surfTrigger = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") && collision.contacts[0].normal.normalized == Vector3.up)
        {
            if (!onGround)
            {
                if (inAir)
                {
                    warriorAnim.SetTrigger("landTrigger");
                    inAir = false;
                }
                onGround = true;
            }
            if (surfTrigger)
            {
                surfTrigger = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            print("Exit velocity: " + collision.relativeVelocity);
            if (Vector3.Dot(collision.relativeVelocity, Vector3.up) > Mathf.Epsilon && !surfTrigger)
            {
                surfTrigger = true;
                surfStart = Time.time;
            }
            onGround = false;
        }
    }
}
