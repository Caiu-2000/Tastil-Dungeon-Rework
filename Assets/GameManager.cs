using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }
    public PlayerMaster Player;
    public PlayerInput InputHandler;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

     
        DontDestroyOnLoad(gameObject);
    }

    public  PlayerMaster GetPlayer()
    {
        return Instance.Player;
    }

    public PlayerInput GetInput()
    {
        return Instance.InputHandler;
    }



}

