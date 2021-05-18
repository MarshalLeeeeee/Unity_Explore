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
        Debug.Log("still or onGround");
        audioSource.Stop();
    }

    public void walk()
    {
        Debug.Log("walk");
        audioSource.clip = walkSound;
        audioSource.Play();
    }

    public void run()
    {
        Debug.Log("run");
        audioSource.clip = runSound;
        audioSource.Play();
    }
}
