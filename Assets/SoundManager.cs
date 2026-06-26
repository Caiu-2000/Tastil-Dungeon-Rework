using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public  class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Sound> sounds;


    public Dictionary<string, float> mixerValue;
    
    public AudioMixerGroup[] AudioMixer;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitSounds();
    }

    private void InitSounds()
    {

        foreach(Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.soundClip;
            sound.Source.outputAudioMixerGroup = sound.AudioMixer;
            sound.Source.volume = sound.volume;
            sound.Source.pitch = sound.pitch;
            sound.Source.loop = sound.IsLoop;
        }
    }


    public void PauseAll()
    {
        foreach (Sound sound in sounds) sound.Source.Pause();
    }

    public void Play(SoundTypes name , bool loop = false)
    {
        Sound sound = FindSound(name);

        if (sound == null) return;
        
        sound.Source.loop = loop;
        sound.Source.Play();
    }

    public void Pause(SoundTypes name, bool loop = false)
    {
        Sound sound = FindSound(name);

        if (sound == null) return;

        sound.Source.loop = loop;
        sound.Source.Pause();
    }




    private Sound FindSound(SoundTypes name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.type == name) return sound;
        }
        return null;
    }

    public void SetAllMixersActive(bool active)
    {
        float newVol;
        if (active)
        {
            newVol = 1;
        }
        else
        {
            newVol = 0.00001f;
        }


        foreach (AudioMixerGroup audioMixer in AudioMixer)
            {
                audioMixer.audioMixer.SetFloat(audioMixer.name, Mathf.Log(newVol) * 20f);
                //_AudioMixerG.audioMixer.SetFloat(mixerGroup, Mathf.Log(vol) * 20f);
            }

    }

}
