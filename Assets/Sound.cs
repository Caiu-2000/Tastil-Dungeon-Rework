using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Sound 
{
    public string soundName;
    public SoundTypes type;
    public AudioClip soundClip;
    public AudioMixerGroup AudioMixer;
    [Range(0f,1f)]public float volume;
    [Range (0f,3f)] public float pitch;
    public bool IsLoop;
    public AudioSource Source;

    
}
