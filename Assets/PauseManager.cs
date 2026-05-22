using System;
using UnityEngine;


public class PauseManager :MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Slider slider;
    [SerializeField]
    public GameObject TheParent;

    private void Start()
    {
        GameManager.Instance.pause = this;
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
