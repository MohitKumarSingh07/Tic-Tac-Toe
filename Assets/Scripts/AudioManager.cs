using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sounds[] sFXSounds;
    public AudioSource sFXSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlaySFX(string clipName)
    {
        Sounds s = Array.Find(sFXSounds, x => x.name == clipName);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sFXSource.clip = s.clip;
            sFXSource.Play();
        }
    }
}
