using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;
    [SerializeField] Image fadeToBlack;
    [SerializeField] float fadeDuration = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Color tempColor = fadeToBlack.color;
        tempColor.a = 0f;
        fadeToBlack.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fade()
    {
        Color tempColor = fadeToBlack.color;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            tempColor.a = alpha;
            fadeToBlack.color = tempColor;
            yield return null;
        }
    }
    public IEnumerator UnFade()
    {
        Color tempColor = fadeToBlack.color;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            tempColor.a = alpha;
            fadeToBlack.color = tempColor;
            yield return null;
        }
    }
}
