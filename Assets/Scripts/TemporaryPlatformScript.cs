using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatformScript : MonoBehaviour

    
{
    public GameObject platform;
    public float startingTime = 2.0f;
    public float toggleTime = 2.0f;
    public GameObject audios;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = audios.GetComponent<AudioSource>();
        InvokeRepeating("togglePlatform", startingTime, toggleTime);
    }

    void togglePlatform()
    {
        audioSrc.Play();
        platform.SetActive(!platform.activeSelf);
    }
}
