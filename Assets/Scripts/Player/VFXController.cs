using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VFXController : MonoBehaviour
{
    public UniversalRendererData rendererData;
    public Material lowHealthMaterial;
    public Material HealMaterial;
    public Material dashMaterial;
    private FullScreenPassRendererFeature fullScreenFeature;
    private Color lowHealthBaseColor;
    private Color dashBaseColor;
    void Start()
    {
        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature.name == "FullScreenPassRendererFeature")
            {
                fullScreenFeature = (FullScreenPassRendererFeature)feature;
                break;
            }
        }
        lowHealthMaterial = new Material(lowHealthMaterial);
        lowHealthBaseColor = lowHealthMaterial.GetColor("_Color");
        dashMaterial = new Material(dashMaterial);
        dashBaseColor = dashMaterial.GetColor("_Color");
        HealMaterial = new Material(HealMaterial);
        fullScreenFeature.SetActive(false);
    }
    public void ShowLowHealth(float intensity = 1f)
    {
        fullScreenFeature.passMaterial = lowHealthMaterial;
        fullScreenFeature.SetActive(true);
        SetHDRIntensity(lowHealthMaterial, lowHealthBaseColor, intensity);
    }
    public void ShowHeal(float duration = 0.3f, float intensity = 1f)
    {
        fullScreenFeature.passMaterial = HealMaterial;
        fullScreenFeature.SetActive(true);
        //SetHDRIntensity(dashMaterial, dashBaseColor, intensity);
        StartCoroutine(HideAfter(duration));
    }
    public void ShowDash(float duration = 0.3f, float intensity = 1f)
    {
        fullScreenFeature.passMaterial = dashMaterial;
        fullScreenFeature.SetActive(true);
        SetHDRIntensity(dashMaterial, dashBaseColor, intensity);
        StartCoroutine(HideAfter(duration));
    }
    public void HideVFX()
    {
        fullScreenFeature.SetActive(false);
    }
    private void SetHDRIntensity(Material mat, Color baseColor, float intensity)
    {
        Color newColor = baseColor * Mathf.Pow(2f, intensity);
        mat.SetColor("_Color", newColor);
    }
    private IEnumerator HideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideVFX();
    }
}