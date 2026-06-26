using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{

    [SerializeField] private SoundTypes[] _soundsPlayName;
    [SerializeField] private bool _loop;

    private void Start()
    {
        foreach (var sound in _soundsPlayName) 
        { 
            SoundManager.instance.Play(sound , _loop);
        }
    }
}
