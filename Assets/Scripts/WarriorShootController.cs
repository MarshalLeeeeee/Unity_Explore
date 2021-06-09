using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShootController : MonoBehaviour
{
    public GameObject logicBullet;
    public GameObject shootGlow;
    public GameObject bulletTrial;
    public float singleShootInterval = 0.5f;
    public float autoShootInterval = 0.1f;
    public float reloadTime = 2.667f;
    public int magSize = 30;
    public float bounceCoolDown = 2.0f;
    public float throughCoolDown = 2.0f;

    private float shootStart = -1.0f;
    private float bounceStart = -1.0f;
    private float throughStart = -1.0f;
    private float reloadStart;
    private int currentMagSize;

    private bool shootMode = true;
    private bool inSemiShoot = false;
    private bool inReloading = false;
    private bool bounceTrigger = false;
    private bool throughTrigger = false;

    private Animator warriorAnim;
    private Transform rifleTransform;
    private Transform spineTransform;
    private RifleSoundController rifleSoundController;
    private FollowPlayer fp;

    // Start is called before the first frame update
    void Start()
    {
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        spineTransform = transform.Find("Hips/Spine");
        warriorAnim = GetComponent<Animator>();
        warriorAnim.SetFloat("singleShootProp", 0.3f);
        warriorAnim.SetFloat("autoShootProp", 0.3f);
        warriorAnim.SetFloat("reloadProp", 0.3f);
        rifleSoundController = FindObjectOfType<RifleSoundController>();
        currentMagSize = magSize;
        fp = FindObjectOfType<FollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // bounce skill
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= bounceStart + bounceCoolDown)
        {
            if (!bounceTrigger) bounceTrigger = true;
            else bounceTrigger = false;
            throughTrigger = false;
        }

        // through skill
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= throughStart + throughCoolDown)
        {
            bounceTrigger = false;
            if (!throughTrigger) throughTrigger = true;
            else throughTrigger = false;
        }


        // shoot
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (shootMode) { shootMode = false; }
            else { shootMode = true; warriorAnim.SetBool("autoShootFlag", false); }
        }
        if (shootMode)
        {
            if (Input.GetMouseButtonDown(0) && (Time.time >= shootStart + singleShootInterval) && !inReloading)
            {
                shootStart = Time.time;
                GameObject bulletObject = Instantiate(logicBullet, fp.transform.position, fp.transform.rotation);
                BulletBehavior bb = bulletObject.GetComponent<BulletBehavior>();
                bb.setShooter(transform);
                Instantiate(shootGlow, rifleTransform.position + rifleTransform.up * (-0.9f) + rifleTransform.right * (-0.9f), shootGlow.transform.rotation);

                if (bounceTrigger)
                {
                    Debug.Log("bounce bullet");
                    bb.setBounce();
                    bounceStart = shootStart;
                    bounceTrigger = false;
                }
                else if (throughTrigger)
                {
                    Debug.Log("through bullet");
                    bb.setThrough();
                    throughStart = shootStart;
                    throughTrigger = false;
                }

                fp.recoil();
                currentMagSize -= 1;
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
                shootStart = Time.time;
                GameObject bulletObject = Instantiate(logicBullet, fp.transform.position, fp.transform.rotation);
                BulletBehavior bb = bulletObject.GetComponent<BulletBehavior>();
                bb.setShooter(transform);
                Instantiate(shootGlow, rifleTransform.position + rifleTransform.up * (-0.9f) + rifleTransform.right * (-0.9f), shootGlow.transform.rotation);

                if (bounceTrigger)
                {
                    Debug.Log("bounce bullet");
                    bb.setBounce();
                    bounceStart = shootStart;
                    bounceTrigger = false;
                }
                else if (throughTrigger)
                {
                    Debug.Log("through bullet");
                    bb.setThrough();
                    throughStart = shootStart;
                    throughTrigger = false;
                }

                fp.recoil();
                currentMagSize -= 1;
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
}
