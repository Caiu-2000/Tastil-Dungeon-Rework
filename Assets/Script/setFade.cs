using UnityEngine;
using UnityEngine.UI;

public class setFade : MonoBehaviour
{
    
    private void Start()
    {
        FadeController.Instance.SetFadeToBlack(this.GetComponent<Image>());
    }
}
