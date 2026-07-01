

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEmitterComponent 
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Sound[] sounds;
 
    [SerializeField]
    private AudioAlbum[] AudioAlbum;
    [SerializeField]
    private AudioMixerGroup AudioMixerGroup;
 

    public void InitializeThis()
    {
     _audioSource.outputAudioMixerGroup = AudioMixerGroup;



        foreach (AudioAlbum album in AudioAlbum)
        {
            album.Source =_audioSource;
            album.Source.outputAudioMixerGroup = album.AudioMixer;
            album.Source.volume = album.volume;
        }

    }

    public void PlaySound(SoundTypes type)
    {
        if (FindSound(type , out Sound sound) )
        {
            Debug.Log(sound.type);
            _audioSource.clip = sound.soundClip;
            _audioSource.Play(); 
        }
    }

    public void PlayRandom(SoundTypes type)
    {
        _audioSource.clip = SoundManager.FindAlbum(type , AudioAlbum ).GetRandomClip();
        _audioSource.Play();
    }


    private bool  FindSound(SoundTypes name , out Sound foundSound)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.type == name)
            {
                foundSound = sound;
                return true;
            }
        }
        foundSound = null;
        return false;
    }









}
