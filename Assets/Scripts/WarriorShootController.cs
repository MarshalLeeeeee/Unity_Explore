using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarriorShootController : MonoBehaviour
{
    public TextMeshProUGUI magText;
    public Image crosshairHit;
    public Image bounceIcon;
    public Image throughIcon;
    public RawImage semiShootImg;
    public RawImage autoShootImg;

    public GameObject logicBullet;
    public GameObject shootGlow;
    public GameObject bulletTrial;
    public float singleShootInterval = 0.5f;
    public float autoShootInterval = 0.1f;
    public float reloadTime = 2.667f;
    public int magSize = 30;
    public float bounceCoolDown = 2.0f;
    public float throughCoolDown = 2.0f;
    public float crosshairHitTime = 1.0f;
    public float dmg = 25.0f;

    private float shootStart = -100.0f;
    private float bounceStart = -100.0f;
    private float throughStart = -100.0f;
    private float reloadStart;
    private int currentMagSize;

    private bool shootMode = true;
    private bool inSemiShoot = false;
    private bool inReloading = false;
    private bool bounceTrigger = false;
    private bool throughTrigger = false;

    private Animator warriorAnim;
    private Transform rifleTransform;
    private RifleSoundController rifleSoundController;
    private FollowPlayer fp;

    private RaycastHit hit;
    private Vector3 shootPoint;

    private Color availableColor = new Color(0.0f, 0.0f, 0.0f);
    private Color cdColor = new Color(0.35f, 0.35f, 0.35f);
    private Color bounceColor = new Color(30.0f / 255.0f, 1.0f, 0.0f);
    private Color throughColor = new Color(148.0f / 255.0f,0.0f,1.0f);

    // Start is called before the first frame update
    void Start()
    {
        rifleTransform = transform.Find("Hips/ArmPosition_Right");
        warriorAnim = GetComponent<Animator>();
        warriorAnim.SetFloat("singleShootProp", 0.3f);
        warriorAnim.SetFloat("autoShootProp", 0.3f);
        warriorAnim.SetFloat("reloadProp", 0.3f);
        rifleSoundController = transform.Find("AssaultRifle").gameObject.GetComponent<RifleSoundController>();
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

        if (Time.time < bounceStart + bounceCoolDown) bounceIcon.color = cdColor;
        else if (bounceTrigger) bounceIcon.color = bounceColor;
        else bounceIcon.color = availableColor;

        if (Time.time < throughStart + throughCoolDown) throughIcon.color = cdColor;
        else if (throughTrigger) throughIcon.color = throughColor;
        else throughIcon.color = availableColor;


        // shoot
        if (Input.GetKeyDown(KeyCode.B))
            {
                if (shootMode) { shootMode = false; }
                else { shootMode = true; warriorAnim.SetBool("autoShootFlag", false); }
            }
        if (shootMode)
        {
            semiShootImg.gameObject.SetActive(true);
            autoShootImg.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0) && (Time.time >= shootStart + singleShootInterval) && !inReloading)
            {
                shootStart = Time.time;
                shootPoint = rifleTransform.position + rifleTransform.up * (-0.9f) + rifleTransform.right * (-0.9f);
                Instantiate(shootGlow, shootPoint, Quaternion.identity);

                Vector3 shootDirection;
                if (Physics.Raycast(new Ray(fp.transform.position + fp.transform.forward, fp.transform.forward), out hit, 1000.0f)) shootDirection = (hit.point - shootPoint).normalized;
                else shootDirection = (fp.transform.position + fp.transform.forward * 10000.0f - shootPoint).normalized;
                GameObject trial = Instantiate(bulletTrial, shootPoint, Quaternion.FromToRotation(Vector3.forward, shootDirection));

                GameObject bulletObject = Instantiate(logicBullet, fp.transform.position + fp.transform.forward * 1.0f, fp.transform.rotation);
                BulletBehavior bb = bulletObject.GetComponent<BulletBehavior>();
                bb.setShooter(transform);
                bb.setTrial(trial);
                bb.setDmg(dmg);

                if (bounceTrigger)
                {
                    bb.setBounce();
                    bounceStart = shootStart;
                    bounceTrigger = false;
                }
                else if (throughTrigger)
                {
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
            semiShootImg.gameObject.SetActive(false);
            autoShootImg.gameObject.SetActive(true);
            if (Input.GetMouseButton(0) && (Time.time >= shootStart + autoShootInterval) && !inSemiShoot && !inReloading)
            {
                shootStart = Time.time;
                shootPoint = rifleTransform.position + rifleTransform.up * (-0.9f) + rifleTransform.right * (-0.9f);
                Instantiate(shootGlow, shootPoint, Quaternion.identity);

                Vector3 shootDirection;
                if (Physics.Raycast(new Ray(fp.transform.position + fp.transform.forward, fp.transform.forward), out hit, 1000.0f)) shootDirection = (hit.point - shootPoint).normalized;
                else shootDirection = (fp.transform.position + fp.transform.forward * 10000.0f - shootPoint).normalized;
                GameObject trial = Instantiate(bulletTrial, shootPoint, Quaternion.FromToRotation(Vector3.forward, shootDirection));

                GameObject bulletObject = Instantiate(logicBullet, fp.transform.position + fp.transform.forward * 1.0f, fp.transform.rotation);
                BulletBehavior bb = bulletObject.GetComponent<BulletBehavior>();
                bb.setShooter(transform);
                bb.setTrial(trial);
                bb.setDmg(dmg);

                if (bounceTrigger)
                {
                    bb.setBounce();
                    bounceStart = shootStart;
                    bounceTrigger = false;
                }
                else if (throughTrigger)
                {
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
        magText.text = currentMagSize.ToString() + " / " + magSize.ToString();
    }

    public void hitFeedback()
    {
        StartCoroutine(activeCrosshair());
    }

    IEnumerator activeCrosshair()
    {
        crosshairHit.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        crosshairHit.gameObject.SetActive(false);
    }
}
