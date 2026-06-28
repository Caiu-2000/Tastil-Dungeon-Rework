using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        // En el menú el cursor SIEMPRE tiene que estar visible y libre
        // para poder clickear los botones. Lo sacamos del guard de abajo a
        // propósito, así también funciona cuando volvés al menú desde el juego.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!GameManager.Instance) return;
        GameManager.Instance.pause.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.IsPaused = false;
    }

    // JUGAR (partida nueva). También sirve para CRÉDITOS si es una escena:
    // le pasás el índice de esa escena desde el OnClick.
    public void LoadLevel(int Level = 1)
    {
        SceneManager.LoadScene(Level);
    }

    // AJUSTES / Controles: abre y cierra el panel con la animación.
    public void Controlspressed()
    {
        _animator.SetTrigger("Open");
    }

    public void CloseControls()
    {
        _animator.SetTrigger("CLose"); // Ojo: el trigger se llama "CLose" en tu Animator.
    }

    // SALIR
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        // Para que SALIR también frene el Play dentro del Editor de Unity.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
