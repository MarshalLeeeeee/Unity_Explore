using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip flySound;
    private bool prevPause = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PauseManager.isPause && !prevPause)
        {
            audioSource.Pause();
            prevPause = true;
        }
        if (!PauseManager.isPause && prevPause)
        {
            audioSource.UnPause();
            prevPause = false;
        }
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

    public void fly()
    {
        audioSource.clip = flySound;
        audioSource.Play();
    }
}
