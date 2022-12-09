using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{

    public AudioMixer audioMixer;
  

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }
}
