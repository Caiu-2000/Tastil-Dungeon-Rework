using UnityEngine;

[DefaultExecutionOrder(10)]
public class PauseManager :MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Slider slider;
    [SerializeField]
    public GameObject TheParent;
    public static PauseManager Instance;
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
        TheParent.gameObject.SetActive(v);
    }
}
