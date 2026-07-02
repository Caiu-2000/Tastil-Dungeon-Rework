

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{


    [Header("Player related")]
    public static GameManager Instance { get; private set; }
    public PlayerMaster Player;
    public PlayerInput InputHandler;
    public UiHandler Ui;



    [Header("Game Control")]
    private InputAction PauseAction;
    private InputAction RestartAction;
    private InputAction DebugAction;
    public bool IsPaused = false;
    public float PendigSens = 25;
    public bool DebugActive = false;
    public PauseManager pause;




    // QUEDA SACAR DE ACA , ESTO ES SOLO PARA LA ZONA DE PRUEBAS
    [Header("Test zone Related")]
    public Transform SpawnPoint;
    public Enemy MelePrefab;





    public float ParryFreezeTime = 0.08f;

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

        if (Keyboard.current.numpad7Key.wasPressedThisFrame)
        {
            if (SpawnPoint) Instantiate(MelePrefab, SpawnPoint);
        }



    }

    public  void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        pause.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
        
    }

    public void Resume()
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
       
    }



    public void LoadLevel(int lvlIndex)
    {
        if (IsPaused) 
        {
            Resume();
            
        }
        if (lvlIndex == 0)
        {
            Resume();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        SceneManager.LoadScene(lvlIndex);
    }

    public void LoadLevel(string lvlName)
    {
        if (IsPaused) { Resume(); }
        SceneManager.LoadScene(lvlName);
    }

    public void ParriedSuccsecsfully()
    {
        
        StartCoroutine(ParryTime());
    }

    private IEnumerator ParryTime()
    {
        Time.timeScale = 0.05f;
        float elapsedtime = 0;
        while (true)
        {
            elapsedtime += Time.unscaledDeltaTime;
            if (elapsedtime > ParryFreezeTime) { break; }
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1f;
    }

    internal void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

