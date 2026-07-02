using System;

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioAlbum
{
   
    public SoundTypes type;
    public AudioClip[] soundClip;
    public AudioMixerGroup AudioMixer;
    [Range(0f, 1f)] public float volume =1.0f;
    public AudioSource Source;

    internal void PlayAudio()
    {
       
        Source.clip = GetRandomClip();
        
        Source.Play();
    }
    public AudioClip GetRandomClip()
    {
       
        return soundClip[UnityEngine.Random.Range(0, soundClip.Length)];
    }


}
