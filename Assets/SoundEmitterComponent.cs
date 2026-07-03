

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEmitterComponent 
{
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected Sound[] sounds;
 
    [SerializeField]
    protected AudioAlbum[] AudioAlbum;
    [SerializeField]
    protected AudioMixerGroup AudioMixerGroup;
 

    public virtual void InitializeThis(Entity ParentEntity= null)
    {
     _audioSource.outputAudioMixerGroup = AudioMixerGroup;

        foreach (AudioAlbum album in AudioAlbum)
        {
            album.Source =_audioSource;
            album.Source.outputAudioMixerGroup = album.AudioMixer;
            album.Source.volume = album.volume;
        }

    }

    public void PlaySound(SoundTypes type , bool ChangePitch = false)
    {

        if (FindSound(type , out Sound sound) )
        {
            Debug.Log(sound.type);
            if (ChangePitch) _audioSource.pitch = Random.Range(0.8f , 1.1f);
            _audioSource.clip = sound.soundClip;
            _audioSource.Play(); 
        }
    }

    public void PlayRandom(SoundTypes type , bool randomitch = false)
    {
        AudioClip clip =  SoundManager.FindAlbum(type , AudioAlbum ).GetRandomClip();
        if (randomitch) _audioSource.pitch = Random.Range(0.8f, 1.1f);
        _audioSource.clip = clip;
        
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
