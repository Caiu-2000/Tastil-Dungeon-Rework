

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public  class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public Sound[] sounds;
    public AudioAlbum[] albums;

    public Dictionary<string, float> mixerValue = new Dictionary<string, float>();
    
    public AudioMixerGroup[] AudioMixer;

    private void Awake()
    {
        if (!instance) instance = this;
        else
        {

            Destroy(gameObject);
        } 

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
        foreach (AudioAlbum album in albums)
        {
            album.Source = gameObject.AddComponent<AudioSource>();
            album.Source.outputAudioMixerGroup = album.AudioMixer;
            album.Source.volume = album.volume;
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
    public void PlayRandom(SoundTypes name)
    {
        AudioAlbum album = FindAlbum(name , albums);
        if (album != null) album.PlayAudio();
    }

    public static AudioAlbum FindAlbum(SoundTypes name, AudioAlbum[] AlbumToSearch)
    {
        foreach (AudioAlbum album in AlbumToSearch) 
        {
       
            if (album.type == name) { return album; } 
        }
        return null;
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
            print(audioMixer.name);
                audioMixer.audioMixer.SetFloat(audioMixer.name, Mathf.Log(newVol) * 20f);
                
            }

    }

    public void StopPlaying(SoundTypes name)
    {
        FindSound(name).Source.Stop();
    }
    private void Update()
    {
        if (Camera.main)
        {
            this.gameObject.transform.position = Camera.main.transform.position;
        }
    }
}
