using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnDelay = 2.0f;
    public GameObject npc;
    public Vector3 npcOrigin;
    public GameObject healthBall;
    public Vector3 healthBallOrigin;
    public GameObject powerBall;
    public Vector3 powerBallOrigin;
    public GameObject shieldBall;
    public Vector3 shieldBallOrigin;

    private GameObject currentNpc = null;
    private GameObject currentHealthBall = null;
    private GameObject currentPowerBall = null;
    private GameObject currentShieldBall = null;

    private bool prepareNpc = false;
    private bool prepareHealthBall = false;
    private bool preparePowerBall = false;
    private bool prepareShieldBall = false;

    // Update is called once per frame
    void Update()
    {
        if (currentNpc == null && !prepareNpc)
        {
            StartCoroutine(instantiateNpc());
        }
        if (currentHealthBall == null && !prepareHealthBall)
        {
            StartCoroutine(instantiateHealthBall());
        }
        if (currentPowerBall == null && !preparePowerBall)
        {
            StartCoroutine(instantiatePowerBall());
        }
        if (currentShieldBall == null && !prepareShieldBall)
        {
            StartCoroutine(instantiateShieldBall());
        }
    }

    IEnumerator instantiateNpc()
    {
        prepareNpc = true;
        yield return new WaitForSeconds(spawnDelay);
        currentNpc = Instantiate(npc, npcOrigin, npc.transform.rotation);
        prepareNpc = false;
        yield return null;
    }

    IEnumerator instantiateHealthBall()
    {
        prepareHealthBall = true;
        yield return new WaitForSeconds(spawnDelay);
        currentHealthBall = Instantiate(healthBall, healthBallOrigin, healthBall.transform.rotation);
        prepareHealthBall = false;
        yield return null;
    }

    IEnumerator instantiatePowerBall()
    {
        preparePowerBall = true;
        yield return new WaitForSeconds(spawnDelay);
        currentPowerBall = Instantiate(powerBall, powerBallOrigin, powerBall.transform.rotation);
        preparePowerBall = false;
        yield return null;
    }

    IEnumerator instantiateShieldBall()
    {
        prepareShieldBall = true;
        yield return new WaitForSeconds(spawnDelay);
        currentShieldBall = Instantiate(shieldBall, shieldBallOrigin, shieldBall.transform.rotation);
        prepareShieldBall = false;
        yield return null;
    }
}
