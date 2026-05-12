using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }
    public PlayerMaster Player;

    private void Awake()
    {
        // Si ya existe una instancia y no es esta, se destruye
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Opcional: Hace que el objeto no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }

    public  PlayerMaster GetPlayer()
    {
        return Instance.Player;
    }

}

