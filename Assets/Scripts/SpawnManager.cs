using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        if (currentNpc == null)
        {
            currentNpc = Instantiate(npc, npcOrigin, npc.transform.rotation);
        }
        if (currentHealthBall == null)
        {
            currentHealthBall = Instantiate(healthBall, healthBallOrigin, healthBall.transform.rotation);
        }
        if (currentPowerBall == null)
        {
            currentPowerBall = Instantiate(powerBall, powerBallOrigin, powerBall.transform.rotation);
        }
        if (currentShieldBall == null)
        {
            currentShieldBall = Instantiate(shieldBall, shieldBallOrigin, shieldBall.transform.rotation);
        }
    }
}
