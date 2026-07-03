
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class PauseManager :MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Slider slider;
    
    
    [SerializeField]
    public GameObject Pause;

    [SerializeField]
    public GameObject Config;
    
    public static PauseManager Instance;
    private bool _audioOn = true;

    [SerializeField] private Image AudioImage;
    [SerializeField] Sprite[] UiSprites;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.pause = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SensChanged()
    {
        
        GameManager.Instance.ChangeSens(slider.value);
    }

    internal void SetActive(bool v)
    {
        if (!Pause) return;
        toggleGeneral(v);
        ToggleConfig(false);
    }


    public void ClosePressed()
    {
        toggleGeneral(false);
        ToggleConfig(true);
        GameManager.Instance.Resume();
    }

    public void GoMenuPressed()
    {
        GameManager.Instance.LoadLevel(0);
    }

    public void GoToLobbyPressed()
    {
        GameManager.Instance.LoadLevel(1);
    }


    public void ToggleVolume()
    {
        _audioOn = !_audioOn;

        if (_audioOn)
        {
            AudioImage.sprite = UiSprites[0];
        }
        else 
        {
            AudioImage.sprite = UiSprites[1];
        }

            SoundManager.instance.SetAllMixersActive(_audioOn);
        
    }

    public void configPressed()
    {
        toggleGeneral(false);
        ToggleConfig(true);
    }

    public void CloseConfigPressed()
    {
        toggleGeneral(true);
        ToggleConfig(false);
    }

    private void toggleGeneral(bool active)
    {
        Pause.GetComponent<GraphicRaycaster>().enabled = active;
        Pause.gameObject.SetActive(active);
    }
    private void ToggleConfig(bool active)
    {
        Config.GetComponent<GraphicRaycaster>().enabled = active;
        Config.gameObject.SetActive(active);
    }
}
