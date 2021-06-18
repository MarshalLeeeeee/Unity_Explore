using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealthController : MonoBehaviour
{
    public float initHealth = 100;
    public float deathHeight = -10.0f;
    public float wallDamage = 50;
    public float powerFeedback = 0.2f;
    public float shieldFeedBack = 0.5f;

    private float maxHealth;
    private float currentHealth;
    private bool alive = true;

    private GameObject warrior;
    private WarriorShootController sc;
    private WarriorPowerController pc;
    private WarriorShieldController shc;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = initHealth;
        currentHealth = maxHealth;
        warrior = GameObject.FindWithTag("Player");
        sc = warrior.GetComponent<WarriorShootController>();
        pc = warrior.GetComponent<WarriorPowerController>();
        shc = warrior.GetComponent<WarriorShieldController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= deathHeight)
        {
            death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            takeDamage(wallDamage);
            checkHealth();
        }
    }

    public void takeDamage(float dmg)
    {
        float oldHealth = currentHealth;
        currentHealth -= dmg;
        checkHealth();
        float actualDmg = oldHealth - currentHealth;
        pc.addPower(actualDmg * powerFeedback);
        sc.hitFeedback();
    }

    public void takeHeal(float heal)
    {
        currentHealth += heal;
        checkHealth();
    }

    private void death()
    {
        currentHealth = 0;
        alive = false;
        shc.addShield(maxHealth * shieldFeedBack);
        Destroy(gameObject);
    }

    private void checkHealth()
    {
        if (currentHealth <= 0)
        {
            death();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
