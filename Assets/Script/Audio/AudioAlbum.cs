using System;

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioAlbum
{
   
    public SoundTypes type;
    public AudioClip[] soundClip;
    public AudioMixerGroup AudioMixer;
    [Range(0f, 1f)] public float volume;
    public AudioSource Source;

    internal void PlayAudio()
    {
        Debug.Log("PlayAudioSeEjecuto");
        Source.clip = soundClip[UnityEngine.Random.Range(0, soundClip.Length)] ;
        Source.Play();
    }
}
