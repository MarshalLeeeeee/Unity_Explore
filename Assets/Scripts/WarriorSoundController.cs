using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void still()
    {
        audioSource.Stop();
    }

    public void walk()
    {
        audioSource.clip = walkSound;
        audioSource.Play();
    }

    public void run()
    {
        audioSource.clip = runSound;
        audioSource.Play();
    }
}
