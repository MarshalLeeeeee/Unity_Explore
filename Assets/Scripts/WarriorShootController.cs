using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShootController : MonoBehaviour
{
    public GameObject bullet;
    public float singleShootInterval = 0.5f;
    public float autoShootInterval = 0.1f;
    public float reloadTime = 2.667f;
    public int magSize = 30;

    private float shootStart = -1.0f;
    private float reloadStart;
    private int currentMagSize;

    private bool shootMode = true;
    private bool inSemiShoot = false;
    private bool inReloading = false;

    private Animator warriorAnim;
    private Transform rifleTransform;
    private RifleSoundController rifleSoundController;

    // Start is called before the first frame update
    void Start()
    {
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        warriorAnim = GetComponent<Animator>();
        warriorAnim.SetFloat("singleShootProp", 0.3f);
        warriorAnim.SetFloat("autoShootProp", 0.3f);
        warriorAnim.SetFloat("reloadProp", 0.3f);
        rifleSoundController = FindObjectOfType<RifleSoundController>();
        currentMagSize = magSize;
    }

    // Update is called once per frame
    void Update()
    {
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
                Instantiate(bullet, rifleTransform.position - 0.9f * rifleTransform.right - 0.9f * rifleTransform.up + 0.01f * rifleTransform.forward, rifleTransform.rotation * Quaternion.Euler(0.0f, 0.0f, 135.0f), transform);
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
}
