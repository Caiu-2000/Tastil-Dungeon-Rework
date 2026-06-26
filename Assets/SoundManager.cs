using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public  class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Sound> sounds;


    public Dictionary<string, float> mixerValue;


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
}
