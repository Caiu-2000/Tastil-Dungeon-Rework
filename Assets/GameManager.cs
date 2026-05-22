using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;



public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }
    public PlayerMaster Player;
    public PlayerInput InputHandler;


    private InputAction PauseAction;
    public bool IsPaused = false;

    public float PendigSens = 25;

    public PauseManager pause;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

     
        DontDestroyOnLoad(gameObject);
        PauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        print("Sos re vivo");
        if (PauseAction.WasPressedThisFrame())
        {
            print("Se apreto");
            if (IsPaused) Resume();
            else Pause();
 
        }
    }

    private void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;   
    }







    public  PlayerMaster GetPlayer()
    {
        return Instance.Player;
    }

    public PlayerInput GetInput()
    {
        return Instance.InputHandler;
    }


    public void ChangeSens(float sens)
    {
        
        Player.GetComponent<PlayerMovement>().rotationspeed = sens;
        print(sens);
    }
}

