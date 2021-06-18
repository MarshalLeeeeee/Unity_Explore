using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DmgTextTransform : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float shrinkSpeed = 1.0f;
    private float dmg = 0.0f;
    private TextMeshProUGUI dmgText;

    private void Start()
    {
        dmgText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dmg > 0.0f) dmgText.text = dmg.ToString();
        transform.position += new Vector3(moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0.0f);
        transform.localScale -= new Vector3(shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime);
    }

    public void setDmg(float d)
    {
        dmg = d;
    }
}
