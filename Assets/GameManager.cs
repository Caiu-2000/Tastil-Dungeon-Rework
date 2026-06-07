

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }
    public PlayerMaster Player;
    public PlayerInput InputHandler;


    private InputAction PauseAction;
    private InputAction RestartAction;
    private InputAction DebugAction;
    public bool IsPaused = false;

    public float PendigSens = 25;
    public bool DebugActive = false;
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
        RestartAction = InputSystem.actions.FindAction("Restart");
        DebugAction = InputSystem.actions.FindAction("DebugButton");

    }


    private void Update()
    {
        // Cada que se resetaba se perdia la referencia y se rompia todo. 
        // Esto es un manotazo de ahogado
        if (!pause && Player) pause = Player.gameObject.GetComponentInChildren<PauseManager>();

        if (PauseAction.WasPressedThisFrame())
        {

            if (IsPaused) Resume();
            else Pause();
 
        }

        if (RestartAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (DebugAction.WasPressedThisFrame())
        {
            DebugActive = !DebugActive;
        }
        

    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        pause.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
        
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause.SetActive(false);
        Time.timeScale = 1;
        
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

