using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float impluse = 100.0f;
    public float maxDestroyTime = 10.0f;
    public GameObject bouncePad;
    public GameObject throughPad;
    public GameObject bulletHit;

    private Transform shooter;
    private Rigidbody rb;
    private bool bounceTrigger = false;
    private bool throughTrigger = false;
    private GameObject trial;
    private float dmg = 10.0f;

    private void Start()
    {
        transform.Rotate(90.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * impluse, ForceMode.Impulse);
        Destroy(gameObject, maxDestroyTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((bounceTrigger || throughTrigger) && collision.transform.tag == "Floor")
        {
            Transform colliderTransform = collision.transform;
            Vector3 projectPoint = Vector3.ProjectOnPlane((collision.GetContact(0).point - colliderTransform.position), colliderTransform.up) + colliderTransform.position;
            if (bounceTrigger)
            {
                Instantiate(bouncePad, projectPoint, Quaternion.FromToRotation(colliderTransform.up, Vector3.up));
            }
            else if (throughTrigger)
            {
                Instantiate(throughPad, projectPoint, Quaternion.FromToRotation(colliderTransform.up, Vector3.up));
            }
        }
        
        if (shooter.tag == "Player")
        {
            if (collision.transform.tag == "NPC")
            {
                collision.gameObject.GetComponent<NpcHealthController>().takeDamage(dmg);
            }
            if (collision.transform.tag == "HealthBall")
            {
                shooter.gameObject.GetComponent<WarriorHealthController>().takeHeal(10.0f);
                Destroy(collision.gameObject);
            }
            if (collision.transform.tag == "PowerBall")
            {
                shooter.gameObject.GetComponent<WarriorPowerController>().addPower(10.0f);
                Destroy(collision.gameObject);
            }
            if (collision.transform.tag == "ShieldBall")
            {
                shooter.gameObject.GetComponent<WarriorShieldController>().addShield(25.0f);
                Destroy(collision.gameObject);
            }
        }

        if (shooter.tag == "NPC" && collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<WarriorHealthController>().takeDamage(dmg);
        }

        Destroy(gameObject, 1.0f);
        if (trial) Destroy(trial);
        Instantiate(bulletHit, collision.GetContact(0).point, Quaternion.identity);
        collision.GetContact(0).thisCollider.isTrigger = true;
    }

    public void setShooter(Transform transform)
    {
        shooter = transform;
    }

    public void setBounce()
    {
        bounceTrigger = true;
    }

    public void setThrough()
    {
        throughTrigger = true;
    }

    public void setTrial(GameObject t)
    {
        trial = t;
    }

    public void setDmg(float d)
    {
        dmg = d;
    }
}
