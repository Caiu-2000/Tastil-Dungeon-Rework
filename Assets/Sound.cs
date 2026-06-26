using UnityEngine;

public enum SoundTypes
{
    Menu,
    Ambient,
    Effects
}

[System.Serializable]
public class Sound : MonoBehaviour
{
    public string soundName;
    public SoundTypes type;
    public AudioClip clip;
    [Range(0f,1f)]public float volume;
    [Range (0f,3f)] public float pitch;
    public bool IsLoop;
    public AudioSource Source;

    
}
