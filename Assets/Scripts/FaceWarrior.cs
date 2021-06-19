using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceWarrior : MonoBehaviour
{
    public float senseRadius;
    public GameObject logicBullet;
    public GameObject bulletTrial;
    public GameObject aimRange;
    public float singleShootInterval = 0.5f;
    public float autoReloadInterval = 3.0f;
    public float reloadTime = 2.667f;
    public int magSize = 30;
    public float dmg = 25.0f;

    private float shootStart = -1.0f;
    private float collideStart = -100.0f;
    private float reloadStart;
    private int currentMagSize;
    private bool inReloading = false;
    private bool sense = false;

    private Animator anim;
    private Transform rifleTransform;
    private Transform spineTransform;
    private GameObject warrior;
    private Transform warriorTransform;
    private Transform spineTransformWarrior;
    private RifleSoundController rifleSoundController;

    private Vector3 shootPoint;
    private RaycastHit hit;
    private float xangle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        spineTransform = transform.Find("Hips/Spine");
        anim = GetComponent<Animator>();
        anim.SetFloat("singleShootProp", 0.3f);
        anim.SetFloat("autoShootProp", 0.3f);
        anim.SetFloat("reloadProp", 0.3f);
        rifleSoundController = transform.Find("AssaultRifle").gameObject.GetComponent<RifleSoundController>();
        currentMagSize = magSize;
        warrior = GameObject.FindWithTag("Player");
        warriorTransform = warrior.transform;
        spineTransformWarrior = warriorTransform.Find("Hips/Spine");
        GameObject ar = Instantiate(aimRange, transform);
        ar.transform.localScale = new Vector3(senseRadius, senseRadius, senseRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.isPause) return;

        Vector3 relative =  warriorTransform.position - transform.position;
        if (sense)
        {
            xangle = Mathf.Rad2Deg * Mathf.Acos(relative.normalized.y);
            Vector3 relativeProj = Vector3.ProjectOnPlane(relative, transform.up).normalized;
            transform.LookAt(transform.position + relativeProj);
            collideStart = Time.time;

            if ((Time.time >= shootStart + singleShootInterval) && !inReloading)
            {
                shootStart = Time.time;
                shootPoint = rifleTransform.position + rifleTransform.up * (-0.9f) + rifleTransform.right * (-0.9f);

                Vector3 shootDirection = (spineTransformWarrior.position - shootPoint).normalized;
                Vector3 shootLogicPoint = spineTransform.position + relative.normalized * 1.0f;

                if (Physics.Raycast(new Ray(shootLogicPoint, relative.normalized), out hit, 1000.0f) && hit.transform.tag == "Player")
                {
                    GameObject trial = Instantiate(bulletTrial, shootPoint, Quaternion.FromToRotation(Vector3.forward, shootDirection));
                    GameObject bulletObject = Instantiate(logicBullet, spineTransform.position + relative.normalized * 1.0f, Quaternion.FromToRotation(Vector3.forward, relative.normalized));
                    BulletBehavior bb = bulletObject.GetComponent<BulletBehavior>();
                    bb.setShooter(transform);
                    bb.setTrial(trial);
                    bb.setDmg(dmg);

                    currentMagSize -= 1;
                    anim.SetTrigger("singleShootTrigger");
                }
            }
        }
        else
        {
            xangle = 90.0f;
            if (currentMagSize < magSize && !inReloading && Time.time > collideStart + autoReloadInterval)
            {
                inReloading = true;
                reloadStart = Time.time;
                anim.SetTrigger("reloadTrigger");
                rifleSoundController.reload();
            }
            if (inReloading && Time.time > reloadStart + reloadTime)
            {
                inReloading = false;
                currentMagSize = magSize;
            }
        }

        if (currentMagSize == 0 && !inReloading)
        {
            inReloading = true;
            reloadStart = Time.time;
            anim.SetTrigger("reloadTrigger");
            rifleSoundController.reload();
        }
        if (inReloading && Time.time > reloadStart + reloadTime)
        {
            inReloading = false;
            currentMagSize = magSize;
        }
    }

    public float getPoseXAngle()
    {
        return xangle - 90.0f;
    }

    public void ifSense(bool flag)
    {
        sense = flag;
    }
}
