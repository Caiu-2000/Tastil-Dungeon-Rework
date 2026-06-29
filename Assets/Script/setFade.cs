using UnityEngine;
using UnityEngine.UI;

public class setFade : MonoBehaviour
{
    
    private void Start()
    {
        if (!FadeController.Instance) return;
        FadeController.Instance.SetFadeToBlack(this.GetComponent<Image>());
    }
}
