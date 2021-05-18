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
    public float surfMinTime = 0.5f;
    public float singleShootInterval = 0.5f;
    public float autoShootInterval = 0.1f;
    public float reloadTime = 2.667f;
    public int magSize = 30;
    
    private float forwardSpeed = 0.0f;
    private float horizonSpeed = 0.0f; 
    private float groundSpeed = 0.0f;
    private float groundSpeedPrev = 0.0f;
    private float yAngle = 0.0f;

    private bool onGround = true;
    private bool inAir = false;
    private bool shootMode = true;
    private bool inSemiShoot = false;
    private bool inReloading = false;

    private float forwardSpeedInAir;
    private float horizonSpeedInAir;

    private bool jumpTrigger = false;
    private bool surfTrigger = false;

    private float surfStart;
    private float shootStart = -1.0f;
    private float reloadStart;

    private int currentMagSize;

    private Animator warriorAnim;
    private Rigidbody warriorRb;
    private Transform rifleTransform;
    private Transform spineTransform;
    private Vector3 rifleToSpinePos;
    private Quaternion rifleToSpineRotation;
    private Quaternion standingPlaneRotation;
    private FollowPlayer fp;
    private WarriorSoundController soundController;
    private RifleSoundController rifleSoundController;

    private void Start()
    {
        warriorAnim = GetComponent<Animator>();
        warriorRb = GetComponent<Rigidbody>();
        soundController = GetComponent<WarriorSoundController>();
        warriorAnim.SetFloat("singleShootProp", 0.3f);
        warriorAnim.SetFloat("autoShootProp", 0.3f);
        warriorAnim.SetFloat("reloadProp", 0.3f);
        currentMagSize = magSize;
        StartCoroutine(getInitPose());
        standingPlaneRotation = Quaternion.identity;
        fp = FindObjectOfType<FollowPlayer>();
        rifleSoundController = FindObjectOfType<RifleSoundController>();
    }

    IEnumerator getInitPose()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        spineTransform = transform.Find("Hips/Spine");
        rifleToSpinePos = spineTransform.worldToLocalMatrix.MultiplyPoint3x4(rifleTransform.position);
        rifleToSpineRotation = Quaternion.Inverse(spineTransform.rotation) * rifleTransform.rotation;
        yield return null;
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
        yAngle = Input.GetAxis("Mouse X") * horizonSensitivity;
        transform.Rotate(0.0f, yAngle, 0.0f);

        float horizonInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // forward speed control
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

        // horizon speed control
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

        // play sound
        if (onGround) groundSpeed = Mathf.Sqrt(Mathf.Pow(forwardSpeed, 2.0f) + Mathf.Pow(horizonSpeed, 2.0f));
        else groundSpeed = 0.0f;
        if (groundSpeedPrev <= Mathf.Epsilon && groundSpeed > horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon)
        {
            // still ground speed to run
            soundController.run();
        }
        else if (groundSpeedPrev <= Mathf.Epsilon && groundSpeed > Mathf.Epsilon)
        {
            // still ground speed to walk
            soundController.walk();
        }
        else if (groundSpeedPrev <= horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon && groundSpeed > horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon)
        {
            // walk to run
            soundController.run();
        }
        else if (groundSpeedPrev > horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon && groundSpeed <= Mathf.Epsilon)
        {
            // run to still
            soundController.still();
        }
        else if (groundSpeedPrev > horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon && groundSpeed <= horizonSpeedMax * Mathf.Sqrt(2.0f) + Mathf.Epsilon)
        {
            // run to walk
            soundController.walk();
        }
        else if (groundSpeedPrev > Mathf.Epsilon && groundSpeed <= Mathf.Epsilon)
        {
            // walk to still
            soundController.still();
        }
        groundSpeedPrev = groundSpeed;

        // translate
        Matrix4x4 planeRotationMatrix = Matrix4x4.Rotate(standingPlaneRotation);
        Vector3 forwardDirection = planeRotationMatrix.MultiplyPoint3x4(transform.forward);
        Vector3 rightDirection = planeRotationMatrix.MultiplyPoint3x4(transform.right);
        transform.Translate(forwardDirection * Time.deltaTime * forwardSpeed, Space.World);
        transform.Translate(rightDirection * Time.deltaTime * horizonSpeed, Space.World);

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
        if (Input.GetMouseButtonDown(1))
        {
            if (shootMode) { shootMode = false; }
            else { shootMode = true; warriorAnim.SetBool("autoShootFlag", false); }
        }
        if (shootMode)
        {
            if (Input.GetMouseButtonDown(0) && (Time.time >= shootStart + singleShootInterval) && !inReloading)
            {
                Instantiate(bullet, rifleTransform.position - 0.9f * rifleTransform.right - 0.9f * rifleTransform.up + 0.01f * rifleTransform.forward, rifleTransform.rotation * Quaternion.Euler(0.0f,0.0f,135.0f), transform);
                currentMagSize -= 1;
                shootStart = Time.time;
                inSemiShoot = true;
                warriorAnim.SetTrigger("singleShootTrigger");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                inSemiShoot = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && (Time.time >= shootStart + autoShootInterval) && !inSemiShoot && !inReloading)
            {
                Instantiate(bullet, rifleTransform.position - 0.9f * rifleTransform.right - 0.9f * rifleTransform.up + 0.01f * rifleTransform.forward, rifleTransform.rotation * Quaternion.Euler(0.0f, 0.0f, 135.0f), transform);
                currentMagSize -= 1;
                shootStart = Time.time;
                warriorAnim.SetBool("autoShootFlag", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                inSemiShoot = false;
                warriorAnim.SetBool("autoShootFlag", false);
            }
        }

        // reload
        if (((Input.GetKeyDown(KeyCode.R) && currentMagSize < magSize) || (currentMagSize == 0)) && !inReloading)
        {
            inReloading = true;
            reloadStart = Time.time;
            warriorAnim.SetTrigger("reloadTrigger");
            rifleSoundController.reload();
        }
        if (inReloading && Time.time > reloadStart + reloadTime)
        {
            inReloading = false;
            currentMagSize = magSize;
        }

        
    }

    private void LateUpdate()
    {
        if (spineTransform)
        {
            spineTransform.RotateAround(spineTransform.position, transform.right, fp.getPoseXAngle());
        }
        if (rifleTransform)
        {
            rifleTransform.position = spineTransform.localToWorldMatrix.MultiplyPoint3x4(rifleToSpinePos);
            rifleTransform.rotation = spineTransform.rotation * rifleToSpineRotation;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Floor") && isStanding(collision))
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
            standingPlaneRotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal.normalized);
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
            standingPlaneRotation = Quaternion.identity;
        }
    }

    private bool isStanding(Collision collision)
    {
        return Mathf.Abs(Vector3.Dot(collision.contacts[0].normal.normalized, Vector3.up)) >= Mathf.Sqrt(3.0f) * 0.5f;
    }
}
