using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    
    public float forwardAcceleration = 1.0f;
    public float horizonAcceleration = 0.25f;
    public float forwardSpeedMax = 10.0f;
    public float backwardSpeedMax = 10.0f;
    public float horizonSpeedMax = 2.0f;
    public float upForce = 5.0f;
    public float horizonSensitivity = 0.5f;
    public bool userInput = true;
    public float jumpProb = 0.0f;
    public GameObject jetSmoke;

    float horizonInput;
    float forwardInput;
    Matrix4x4 planeRotationMatrix; // local coordinate to surface coordinate
    private Quaternion standingPlaneRotation; // local coordinate to surface coordinate
    private float forwardSpeed = 0.0f; // forward velocity in local surface coordinate
    private float horizonSpeed = 0.0f; // horizon velocity in local surface coordinate
    Vector3 forwardDirection; // forward direct along surface in world coordinate
    Vector3 horizonDirection; // horizon direct along surface in world coordinate
    private Vector3 forwardVelocity = new Vector3(0.0f, 0.0f, 0.0f); // forward accelaration along surface in world coordinate
    private Vector3 horizonVelocity = new Vector3(0.0f, 0.0f, 0.0f); // horizon accelaration along surface in world coordinate
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f); // velocity in world coordinate
    private Vector3 velocityLocal = new Vector3(0.0f, 0.0f, 0.0f); // velocity in surface coordinate

    private float forwardSpeedInAir; // control inAir animation
    private float horizonSpeedInAir; // control inAir animation

    private float groundSpeed = 0.0f; // control walking sound
    private float groundSpeedPrev = 0.0f; // control walking sound

    private float yAngle = 0.0f; // horizon angle

    private bool onGround = true; // if player leaves the surface
    private bool jumpTrigger = false; // if jump is triggered
    
    private Animator warriorAnim; // warrior animator
    private Rigidbody warriorRb; // warrior rigidbody
    private Transform rifleTransform; // rifle gun transform
    private Transform spineTransform; // spine transform
    private Vector3 rifleToSpinePos; // relative position of rifle to spine
    private Quaternion rifleToSpineRotation; // relative rotation of rifle to spine
    private Quaternion jetToSpineRotation; // relative rotation of jet to spine

    private FollowPlayer fp; // get vertical angle from camera view
    private WarriorSoundController soundController; // get sound controller to trigger foot step sound

    private GameObject leftJetSmoke;
    private GameObject rightJetSmoke;


    private void Start()
    {
        warriorAnim = GetComponent<Animator>();
        warriorRb = GetComponent<Rigidbody>();
        soundController = GetComponent<WarriorSoundController>();
        StartCoroutine(getInitPose());
        standingPlaneRotation = Quaternion.identity;
        fp = FindObjectOfType<FollowPlayer>();
    }

    IEnumerator getInitPose()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        spineTransform = transform.Find("Hips/Spine");
        rifleToSpinePos = spineTransform.worldToLocalMatrix.MultiplyPoint3x4(rifleTransform.position);
        rifleToSpineRotation = Quaternion.Inverse(spineTransform.rotation) * rifleTransform.rotation;
        jetToSpineRotation = Quaternion.Inverse(spineTransform.rotation) * jetSmoke.transform.rotation;
        yield return null;
    }

    private void FixedUpdate()
    {
        velocity = warriorRb.velocity;
        velocityLocal = Matrix4x4.Rotate(Quaternion.FromToRotation(transform.forward, Vector3.forward)).MultiplyPoint3x4(velocity);
        velocityLocal = planeRotationMatrix.inverse.MultiplyPoint3x4(velocityLocal);

        warriorRb.AddForce(forwardVelocity, ForceMode.Force);
        warriorRb.AddForce(horizonVelocity, ForceMode.Force);
        velocityLocal = new Vector3(Mathf.Clamp(velocityLocal.x, -horizonSpeedMax, horizonSpeedMax), velocityLocal.y, Mathf.Clamp(velocityLocal.z, -backwardSpeedMax, forwardSpeedMax));
        velocity = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.forward, transform.forward)).MultiplyPoint3x4(planeRotationMatrix.MultiplyPoint3x4(velocityLocal));
        warriorRb.velocity = velocity;

        forwardSpeed = velocityLocal.z;
        horizonSpeed = velocityLocal.x;

        if (jumpTrigger)
        {
            warriorRb.AddForce(upForce * transform.up, ForceMode.Impulse);
            jumpTrigger = false;
        }
    }

    private void Update()
    {
        // mouse movement
        if (userInput) yAngle = Input.GetAxis("Mouse X") * horizonSensitivity;
        transform.Rotate(0.0f, yAngle, 0.0f);

        // movement control
        if (userInput)
        {
            horizonInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizonInput = Random.Range(-1.0f, 1.0f);
            forwardInput = Random.Range(-1.0f, 1.0f);
        }


        planeRotationMatrix = Matrix4x4.Rotate(standingPlaneRotation);
        forwardDirection = planeRotationMatrix.MultiplyPoint3x4(transform.forward);
        horizonDirection = planeRotationMatrix.MultiplyPoint3x4(transform.right);

        forwardVelocity = forwardDirection * forwardInput * forwardAcceleration;
        horizonVelocity = horizonDirection * horizonInput * horizonAcceleration;

        // play sound
        if (onGround) groundSpeed = Mathf.Sqrt(Mathf.Pow(forwardSpeed, 2.0f) + Mathf.Pow(horizonSpeed, 2.0f));
        else groundSpeed = -1.0f;
        if (groundSpeedPrev < 0.0f && groundSpeed >= 0.0f)
        {
            // fly to still
            soundController.still();
        }
        else if (groundSpeedPrev <= 0.1f && groundSpeed > horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f)
        {
            // still or fly to run
            soundController.run();
        }
        else if (groundSpeedPrev <= 0.1f && groundSpeed > 0.1f)
        {
            // still or fly to walk
            soundController.walk();
        }
        else if (groundSpeedPrev <= horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f && groundSpeed > horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f)
        {
            // walk to run
            soundController.run();
        }
        else if (groundSpeedPrev >= 0.0f && groundSpeed < 0.0f)
        {
            // any to fly
            soundController.fly();
        }
        else if (groundSpeedPrev > horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f && groundSpeed <= 0.1f)
        {
            // run to still
            soundController.still();
        }
        else if (groundSpeedPrev > horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f && groundSpeed <= horizonSpeedMax * Mathf.Sqrt(2.0f) + 0.1f)
        {
            // run to walk
            soundController.walk();
        }
        else if (groundSpeedPrev > 0.1f && groundSpeed <= 0.1f)
        {
            // walk to still
            soundController.still();
        }
        groundSpeedPrev = groundSpeed;


        // inAir animation pose control
        if (!onGround)
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
        if (userInput)
        {
            if (Input.GetKeyDown(KeyCode.Space) && onGround) jumpTrigger = true;
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) < jumpProb && onGround) jumpTrigger = true;
        }

        if (!onGround)
        {
            leftJetSmoke.transform.position = spineTransform.position + spineTransform.up * (-0.4f) + spineTransform.forward * (0.2f);
            leftJetSmoke.transform.rotation = spineTransform.rotation * jetToSpineRotation;
            rightJetSmoke.transform.position = spineTransform.position + spineTransform.up * (-0.4f) + spineTransform.forward * (-0.2f);
            rightJetSmoke.transform.rotation = spineTransform.rotation * jetToSpineRotation;
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
                onGround = true;
                Destroy(leftJetSmoke);
                Destroy(rightJetSmoke);
            }
            standingPlaneRotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal.normalized);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            if (onGround)
            {
                onGround = false;
                leftJetSmoke = Instantiate(jetSmoke, spineTransform.position + spineTransform.up * (-0.4f) + spineTransform.forward * (0.2f), spineTransform.rotation * jetToSpineRotation);
                rightJetSmoke = Instantiate(jetSmoke, spineTransform.position + spineTransform.up * (-0.4f) + spineTransform.forward * (-0.2f), spineTransform.rotation * jetToSpineRotation);
                forwardSpeedInAir = forwardSpeed;
                horizonSpeedInAir = horizonSpeed;
            }
            standingPlaneRotation = Quaternion.identity;
        }
    }

    private bool isStanding(Collision collision)
    {
        return Mathf.Abs(Vector3.Dot(collision.contacts[0].normal.normalized, Vector3.up)) >= Mathf.Sqrt(3.0f) * 0.5f;
    }
}
