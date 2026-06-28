using System.Collections;
using UnityEngine;

public class PresentationController : MonoBehaviour
{
    // static = sobrevive al recargar la escena, pero se reinicia
    // cuando cerrás y volvés a abrir el juego.
    private static bool yaSeMostro = false;

    private void Awake()
    {
        if (yaSeMostro)
        {
            // Ya se mostró en esta sesión: la saltamos.
            gameObject.SetActive(false);
            return;
        }

        // Primera vez desde que abriste el juego: se muestra.
        yaSeMostro = true;
        StartCoroutine(VideoCloser());
    }
    private void Update()
    {
        print(Time.timeScale);
    }



    private IEnumerator VideoCloser()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
