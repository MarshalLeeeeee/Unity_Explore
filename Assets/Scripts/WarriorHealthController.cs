using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorHealthController : MonoBehaviour
{
    public int initHealth = 100;
    public float deathHeight = -10.0f;
    public int wallDamage = 50;
    
    private int maxHealth;
    private int currentHealth;
    private bool alive = true;
    private HealthSoundController healthSound;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = initHealth;
        currentHealth = maxHealth;
        healthSound = FindObjectOfType<HealthSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= deathHeight)
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

    public void takeDamage(int dmg)
    {
        currentHealth -= dmg;
        healthSound.takeDamage();
        checkHealth();
    }

    public void takeHeal(int heal)
    {
        currentHealth += heal;
        healthSound.takeHeal();
        checkHealth();
    }

    private void death()
    {
        currentHealth = 0;
        alive = false;
        Debug.Log("Death");
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
