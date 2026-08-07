using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Sprite[] NormalSprites;
    [SerializeField] private Sprite[] SelectedSprites;

    private Image[] ButtonImages = new Image[3];
    [SerializeField] private Button[] Buttons;
    [SerializeField] private GameObject[] Panels;



    private void Start()
    {
        for (int x =0; x < Buttons.Length; x++) 
        {
            ButtonImages[x] = Buttons[x].GetComponent<Image>();
        }
    }


    public void PressedOption(int OptionIndex)
    {
        for (int i = 0; i < Buttons.Length; i++) 
        {
            Panels[i].SetActive(false);
            
            ButtonImages[i].sprite = NormalSprites[i];
            
        
        }

        Panels[OptionIndex].SetActive(true);
        ButtonImages[OptionIndex].sprite = SelectedSprites[OptionIndex];

    }


}
