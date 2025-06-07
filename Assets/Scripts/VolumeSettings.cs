using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    public AudioSource AudioSource;
    private float musicVolume = 1f; 

    void Start()
    {
        AudioSource.Play();
    }

    void Update()
    {
        AudioSource.volume = musicVolume;
    }

    public void uptateVolume(float volume)
    {
        musicVolume = volume;
    }
}
