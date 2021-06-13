using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShieldController : MonoBehaviour
{
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
    private Color shieldColor_4 = new Color(1.0f, 215.0f / 255.0f, 0.0f, 100.0f / 255.0f); // gold

    private Material materialArm;
    private Material materialBackpack;
    private Material materialBody;
    private Material materialHead;
    private Material materialLeg;

    private WarriorController wc;

    // Start is called before the first frame update
    void Start()
    {
        materialArm = transform.Find("Arm1").gameObject.GetComponent<Renderer>().material;
        materialBackpack = transform.Find("Backpack1").gameObject.GetComponent<Renderer>().material;
        materialBody = transform.Find("Body1").gameObject.GetComponent<Renderer>().material;
        materialHead = transform.Find("head1").gameObject.GetComponent<Renderer>().material;
        materialLeg = transform.Find("Leg1").gameObject.GetComponent<Renderer>().material;
        wc = gameObject.GetComponent<WarriorController>();
    }

    // Update is called once per frame
    void Update()
    {
        int oldLevel = currentLevel;
        if (0.0f <= currentShield && currentShield < shieldLevel_1)
        {
            materialArm.color = shieldColor_0;
            materialBackpack.color = shieldColor_0;
            materialBody.color = shieldColor_0;
            materialHead.color = shieldColor_0;
            materialLeg.color = shieldColor_0;
            currentLevel = 0;
        }
        else if (shieldLevel_1 <= currentShield && currentShield < shieldLevel_2)
        {
            materialArm.color = shieldColor_1;
            materialBackpack.color = shieldColor_1;
            materialBody.color = shieldColor_1;
            materialHead.color = shieldColor_1;
            materialLeg.color = shieldColor_1;
            currentLevel = 1;
        }
        else if (shieldLevel_2 <= currentShield && currentShield < shieldLevel_3)
        {
            materialArm.color = shieldColor_2;
            materialBackpack.color = shieldColor_2;
            materialBody.color = shieldColor_2;
            materialHead.color = shieldColor_2;
            materialLeg.color = shieldColor_2;
            currentLevel = 2;
        }
        else if (shieldLevel_3 <= currentShield && currentShield < shieldLevel_4)
        {
            materialArm.color = shieldColor_3;
            materialBackpack.color = shieldColor_3;
            materialBody.color = shieldColor_3;
            materialHead.color = shieldColor_3;
            materialLeg.color = shieldColor_3;
            currentLevel = 3;
        }
        else if (shieldLevel_4 <= currentShield)
        {
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
        }
    }

    public void addShield(float sh)
    {
        currentShield += sh;
    }
}
