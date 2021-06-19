using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorHealthController : MonoBehaviour
{
    public GameObject healthBar;
    public float initHealth = 100.0f;
    public float deathHeight = -10.0f;
    public float wallDamage = 50.0f;
    public float healthSoundInterval = 20.0f;
    public float[] shieldAbsorbtion;
    private float currentShieldAbsorbtion;
    public static bool alive = true;

    private float maxHealth;
    private float prevHealthSound;
    private float currentHealth;
    private HealthSoundController healthSound;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = initHealth;
        prevHealthSound = maxHealth;
        currentHealth = maxHealth;
        currentShieldAbsorbtion = shieldAbsorbtion[0];
        healthSound = transform.Find("head1").gameObject.GetComponent<HealthSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= deathHeight) death();
        if (currentHealth > prevHealthSound) prevHealthSound = currentHealth;

        // health bar UI
        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
    }

    private void OnEnable()
    {
        alive = true;
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
        currentHealth -= dmg * currentShieldAbsorbtion;
        if (currentHealth + healthSoundInterval < prevHealthSound)
        {
            healthSound.takeDamage();
            prevHealthSound = currentHealth;
        }
        checkHealth();
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

    public void updateShield(int i)
    {
        currentShieldAbsorbtion = shieldAbsorbtion[i];
    }
}
