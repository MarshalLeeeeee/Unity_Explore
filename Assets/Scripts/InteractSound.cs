using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{
    public AudioClip crashSound;
    public AudioClip newSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            audioSource.PlayOneShot(crashSound, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AudioClip oldClip = audioSource.clip;
            audioSource.clip = newSound;
            newSound = oldClip;
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            audioSource.UnPause();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            audioSource.Stop();
        }
    }
}
