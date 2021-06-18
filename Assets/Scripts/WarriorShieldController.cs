using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorShieldController : MonoBehaviour
{
    public GameObject shieldBar;
    public float shieldLevel_1;
    public float shieldLevel_2;
    public float shieldLevel_3;
    public float shieldLevel_4;
    private float currentShield = 0.0f;
    private int currentLevel = 0;

    private Color shieldColor_0 = new Color(1.0f, 1.0f, 1.0f, 100.0f / 255.0f); // white
    private Color shieldColor_1 = new Color(0.0f, 0.0f, 1.0f, 100.0f / 255.0f); // blue
    private Color shieldColor_2 = new Color(148.0f / 255.0f, 0.0f, 211.0f / 255.0f, 100.0f / 255.0f); // purple
    private Color shieldColor_3 = new Color(1.0f, 105.0f / 255.0f, 180.0f / 255.0f, 100.0f / 255.0f); // pink
    private Color shieldColor_4 = new Color(1.0f, 140.0f / 255.0f, 0.0f, 100.0f / 255.0f); // orange

    private Color shieldBarColor_0 = new Color(1.0f, 1.0f, 1.0f, 1.0f); // white
    private Color shieldBarColor_1 = new Color(0.0f, 0.0f, 1.0f, 1.0f); // blue
    private Color shieldBarColor_2 = new Color(148.0f / 255.0f, 0.0f, 211.0f / 255.0f, 1.0f); // purple
    private Color shieldBarColor_3 = new Color(1.0f, 105.0f / 255.0f, 180.0f / 255.0f, 1.0f); // pink
    private Color shieldBarColor_4 = new Color(1.0f, 140.0f / 255.0f, 0.0f, 1.0f); // orange

    private Material materialArm;
    private Material materialBackpack;
    private Material materialBody;
    private Material materialHead;
    private Material materialLeg;

    private WarriorController wc;
    private WarriorHealthController whc;
    private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        materialArm = transform.Find("Arm1").gameObject.GetComponent<Renderer>().material;
        materialBackpack = transform.Find("Backpack1").gameObject.GetComponent<Renderer>().material;
        materialBody = transform.Find("Body1").gameObject.GetComponent<Renderer>().material;
        materialHead = transform.Find("head1").gameObject.GetComponent<Renderer>().material;
        materialLeg = transform.Find("Leg1").gameObject.GetComponent<Renderer>().material;
        wc = gameObject.GetComponent<WarriorController>();
        whc = gameObject.GetComponent<WarriorHealthController>();
        bar = shieldBar.transform.Find("Bar").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int oldLevel = currentLevel;
        if (0.0f <= currentShield && currentShield < shieldLevel_1)
        {
            bar.color = shieldBarColor_0;
            shieldBar.transform.localScale = new Vector3(currentShield / shieldLevel_1, 1.0f, 1.0f);
            materialArm.color = shieldColor_0;
            materialBackpack.color = shieldColor_0;
            materialBody.color = shieldColor_0;
            materialHead.color = shieldColor_0;
            materialLeg.color = shieldColor_0;
            currentLevel = 0;
        }
        else if (shieldLevel_1 <= currentShield && currentShield < shieldLevel_2)
        {
            bar.color = shieldBarColor_1;
            shieldBar.transform.localScale = new Vector3((currentShield - shieldLevel_1) / (shieldLevel_2 - shieldLevel_1), 1.0f, 1.0f);
            materialArm.color = shieldColor_1;
            materialBackpack.color = shieldColor_1;
            materialBody.color = shieldColor_1;
            materialHead.color = shieldColor_1;
            materialLeg.color = shieldColor_1;
            currentLevel = 1;
        }
        else if (shieldLevel_2 <= currentShield && currentShield < shieldLevel_3)
        {
            bar.color = shieldBarColor_2;
            shieldBar.transform.localScale = new Vector3((currentShield - shieldLevel_2) / (shieldLevel_3 - shieldLevel_2), 1.0f, 1.0f);
            materialArm.color = shieldColor_2;
            materialBackpack.color = shieldColor_2;
            materialBody.color = shieldColor_2;
            materialHead.color = shieldColor_2;
            materialLeg.color = shieldColor_2;
            currentLevel = 2;
        }
        else if (shieldLevel_3 <= currentShield && currentShield < shieldLevel_4)
        {
            bar.color = shieldBarColor_3;
            shieldBar.transform.localScale = new Vector3((currentShield - shieldLevel_3) / (shieldLevel_4 - shieldLevel_3), 1.0f, 1.0f);
            materialArm.color = shieldColor_3;
            materialBackpack.color = shieldColor_3;
            materialBody.color = shieldColor_3;
            materialHead.color = shieldColor_3;
            materialLeg.color = shieldColor_3;
            currentLevel = 3;
        }
        else if (shieldLevel_4 <= currentShield)
        {
            bar.color = shieldBarColor_4;
            shieldBar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            materialArm.color = shieldColor_4;
            materialBackpack.color = shieldColor_4;
            materialBody.color = shieldColor_4;
            materialHead.color = shieldColor_4;
            materialLeg.color = shieldColor_4;
            currentLevel = 4;
        }
        if (oldLevel != currentLevel)
        {
            wc.updateShield(currentLevel);
            whc.updateShield(currentLevel);
        }
    }

    public void addShield(float sh)
    {
        currentShield += sh;
    }
}
