using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings Instance;

    public AudioSource audioSource;
    private float musicVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSource.volume = musicVolume;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); 
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return musicVolume;
    }
}

