using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float forwardSpeedMax;
    public float forwardAcceleration;
    public float turnSpeed;
    public float jumpForce;

    private float forwardSpeed;
    private Rigidbody rb;
    private bool onGround;
    private bool shouldJump;

    private void Start()
    {
        forwardSpeed = 0.0f;
        rb = GetComponent<Rigidbody>();
        onGround = true;
        shouldJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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
        forwardSpeed = Mathf.Clamp(forwardSpeed, -forwardSpeedMax, forwardSpeedMax);

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * forwardSpeed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            shouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            onGround = false;
            shouldJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            onGround = true;
        }
    }
}
