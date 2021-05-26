using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSoundController : MonoBehaviour
{
    public AudioClip damageClip;
    public AudioClip healClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void takeDamage()
    {
        audioSource.PlayOneShot(damageClip);
    }

    public void takeHeal()
    {
        audioSource.PlayOneShot(healClip);
    }
}
