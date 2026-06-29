using JetBrains.Annotations; // Esto de donde salio xD
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10)]
public class PauseManager :MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Slider slider;
    [SerializeField]
    public GameObject TheParent;
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
        print(slider.value);
        GameManager.Instance.ChangeSens(slider.value);
    }

    internal void SetActive(bool v)
    {
        if (!TheParent) return;
        TheParent.gameObject.SetActive(v);
    }


    public void ClosePressed()
    {
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


}
