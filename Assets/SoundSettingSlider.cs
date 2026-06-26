using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent (typeof(Slider))]
public class SoundSettingSlider : MonoBehaviour
{

    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public string mixerGroup;
    [SerializeField] private AudioMixerGroup _AudioMixerG;
    private SoundManager SoundM;

    private void Start()
    {
        if(!SoundManager.instance) { print("Te olvidaste el manager"); }
        else
        {
            SoundM = SoundManager.instance;
            mixerGroup = _AudioMixerG.name;
            if (!SoundM.mixerValue.ContainsKey(mixerGroup))
            {
                SoundM.mixerValue[mixerGroup] = slider.value;
            } 
        }
    }


}
