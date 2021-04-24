using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public GameObject bullet;
    public float forwardAcceleration = 1.0f;
    public float forwardSpeedMax = 10.0f;
    public float horizonAcceleration = 0.25f;
    public float horizonSpeedMax = 2.0f;
    public float upForce = 5.0f;
    public float horizonSensitivity = 0.5f;
    public float verticalSensitivity = 0.5f;
    public float surfMinTime = 0.5f;
    public float shootInterval = 0.5f;

    private bool shootMode = true;
    private float shootTime = -1.0f;
    private bool inSemiShoot = false;
    private Animator warriorAnim;
    private Rigidbody warriorRb;
    private float forwardSpeed = 0.0f;
    private float horizonSpeed = 0.0f;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;
    private bool onGround = true;
    private bool inAir = false;
    private float forwardSpeedInAir;
    private float horizonSpeedInAir;
    private bool jumpTrigger = false;
    private bool surfTrigger = false;
    private float surfStart;
    private Quaternion initSpineRotation;
    private Transform rifleTransform;
    private Transform rightIndexTransform;

    private void Start()
    {
        warriorAnim = GetComponent<Animator>();
        warriorRb = GetComponent<Rigidbody>();
        initSpineRotation = transform.Find("Hips/Spine").localRotation;
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        rightIndexTransform = transform.Find("Hips/Spine/Chest/Shoulder_Right/UpperArm_Right/LowerArm_Right/Hand_Right/Index_Proximal_Right/Index_Intermediate_Right");
    }

    private void FixedUpdate()
    {
        if (jumpTrigger)
        {
            warriorRb.AddForce(upForce * transform.up, ForceMode.Acceleration);
            jumpTrigger = false;
            inAir = true;
            forwardSpeedInAir = forwardSpeed;
            horizonSpeedInAir = horizonSpeed;
        }
    }

    private void Update()
    {
        // surf detector
        if (surfTrigger && Time.time > surfStart + surfMinTime)
        {
            inAir = true;
            forwardSpeedInAir = forwardSpeed;
            horizonSpeedInAir = horizonSpeed;
            surfTrigger = false;
        }

        // mouse movement
        xAngle += Input.GetAxis("Mouse Y") * verticalSensitivity;
        yAngle = Input.GetAxis("Mouse X") * horizonSensitivity;
        transform.Rotate(0.0f, yAngle, 0.0f);

        float horizonInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // forward control
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

        // horizon control
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

        // inAir animation pose control
        if (inAir)
        {
            warriorAnim.SetFloat("zVelocity", forwardSpeedInAir);
            warriorAnim.SetFloat("xVelocity", horizonSpeedInAir);
            forwardSpeedInAir *= 0.95f;
            horizonSpeedInAir *= 0.95f;
        }
        else
        {
            warriorAnim.SetFloat("zVelocity", forwardSpeed);
            warriorAnim.SetFloat("xVelocity", horizonSpeed);
        }
        
        // jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jumpTrigger = true;
        }

        // shoot
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (shootMode) shootMode = false;
            else shootMode = true;
        }
        if (shootMode)
        {
            if (Input.GetMouseButtonDown(0) && (Time.time >= shootTime + shootInterval))
            {
                Instantiate(bullet, rifleTransform.position - 0.9f * rifleTransform.right - 0.9f * rifleTransform.up + 0.01f * rifleTransform.forward, rifleTransform.rotation * Quaternion.Euler(0.0f,0.0f,135.0f));
                shootTime = Time.time;
                inSemiShoot = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                inSemiShoot = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && (Time.time >= shootTime + shootInterval) && !inSemiShoot)
            {
                Instantiate(bullet, rifleTransform.position - 0.9f * rifleTransform.right - 0.9f * rifleTransform.up + 0.01f * rifleTransform.forward, rifleTransform.rotation * Quaternion.Euler(0.0f, 0.0f, 135.0f));
                shootTime = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                inSemiShoot = false;
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        warriorAnim.SetBoneLocalRotation(HumanBodyBones.Spine, initSpineRotation * Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(xAngle, -90.0f, 90.0f)));
    }

    private void LateUpdate()
    {
        rifleTransform.Rotate(0.0f, 0.0f, Mathf.Clamp(-xAngle, -90.0f, 90.0f));
        rifleTransform.position = rightIndexTransform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") && collision.contacts[0].normal.normalized == Vector3.up)
        {
            if (!onGround)
            {
                if (inAir)
                {
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
            if (Vector3.Dot(collision.relativeVelocity, Vector3.up) > Mathf.Epsilon && !surfTrigger)
            {
                surfTrigger = true;
                surfStart = Time.time;
            }
            onGround = false;
        }
    }
}
