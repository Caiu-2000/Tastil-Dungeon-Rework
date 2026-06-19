using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator _animator;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
