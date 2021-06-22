using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleSoundController : MonoBehaviour
{
    public AudioClip reloadClip;
    private AudioSource audioSource;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PauseManager.isPause) { audioSource.Pause(); paused = true; }
        else
        {
            if (paused) audioSource.UnPause();
        }
    }

    public void reload()
    {
        audioSource.PlayOneShot(reloadClip);
    }
}
