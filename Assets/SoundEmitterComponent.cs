
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEmitterComponent 
{
   
    private AudioSource _audioSource = new AudioSource();
    [SerializeField]
    private Sound[] sounds;
    [SerializeField]
    private AudioAlbum[] AudioAlbum;
    [SerializeField]
    private AudioMixerGroup AudioMixerGroup;






}
