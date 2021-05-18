using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleSoundController : MonoBehaviour
{
    public AudioClip reloadClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void reload()
    {
        audioSource.PlayOneShot(reloadClip);
    }
}
