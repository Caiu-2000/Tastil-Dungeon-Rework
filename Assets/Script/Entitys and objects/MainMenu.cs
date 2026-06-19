using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator _animator;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (!GameManager.Instance) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.pause.SetActive(false);
        Time.timeScale = 1;

        GameManager.Instance.IsPaused = false;
    }


    public void Controlspressed()
    {
        _animator.SetTrigger("Open");
    }
    public void CloseControls()
    {
        _animator.SetTrigger("CLose");
    }
    
    public void LoadLevel(int Level =1)
    {
        SceneManager.LoadScene(Level);
    }

}
