using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorPowerController : MonoBehaviour
{
    public GameObject powerBar;
    public RawImage ultFace;
    public GameObject ultEffect;
    public float powerSpeed = 1.0f;
    public float ultSpeed = 20.0f;
    public float shootAmp = 1.5f;
    public float speedAmp = 2.0f;

    private float currentPower = 0.0f;
    private float maxPower = 100.0f;
    private bool usingUlt = false;
    private Image bar;
    private Color initColor;
    private Color ultColor;

    private Color availableColor = new Color(0.0f, 0.0f, 0.0f);
    private Color cdColor = new Color(0.35f, 0.35f, 0.35f);

    private WarriorShieldController wshc;
    private WarriorShootController wsc;
    private WarriorController wc;

    private void Start()
    {
        bar = powerBar.transform.Find("Bar").gameObject.GetComponent<Image>();
        initColor = bar.color;
        ultColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        wshc = gameObject.GetComponent<WarriorShieldController>();
        wsc = gameObject.GetComponent<WarriorShootController>();
        wc = gameObject.GetComponent<WarriorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.isPause) return;

        if (Input.GetKeyDown(KeyCode.Z) && ifFullPower() && !usingUlt) startUlt();

        if (usingUlt || ifFullPower()) ultFace.color = availableColor;
        else ultFace.color = cdColor;

        if (usingUlt)
        {
            // cost of power
            currentPower -= ultSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
            if (currentPower == 0.0f) endUlt();
        }
        else
        {
            // nature growth of power
            currentPower += powerSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
        }

        // power bar UI
        powerBar.transform.localScale = new Vector3(currentPower / maxPower, 1.0f, 1.0f);
    }

    public void addPower(float p)
    {
        if (!usingUlt)
        {
            currentPower += p;
            currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
        }
    }

    private bool ifFullPower()
    {
        return currentPower == maxPower;
    }

    private void startUlt()
    {
        usingUlt = true;
        ultEffect.SetActive(true);
        bar.color = ultColor;
        wsc.setShootAmp(shootAmp);
        wc.startUlt(speedAmp);
        wshc.startUlt();
    }

    private void endUlt()
    {
        usingUlt = false;
        ultEffect.SetActive(false);
        bar.color = initColor;
        wsc.setShootAmp(1.0f);
        wc.endUlt();
        wshc.endUlt();
    }
}
