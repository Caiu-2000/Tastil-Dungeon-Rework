using UnityEngine;

public class PerkManager : MonoBehaviour
{
    //EN ESTE SCRIPT LA NOMENCLATURA VA A DICIEMBRE

    public static PerkManager Instance { get; private set; }
 
    public delegate void enemyDead(Enemy dead); 
    public delegate void EnemyHitted(Enemy hitted = null, float Damage = 0.0f);
    
    
    //DESPUES VAMOS A TENER UNO DE PLAYER HITTED
    //DAMAGED ES PARA CUANDO RECIBE DAÒO
    //HITTED VA A SER PARA SABER QUIEN LO GOLPEA POR LOGICA TODAVIA NO SE PUEDE
    public delegate void PlayerDamaged(float dam = 0 , Enemy who = null);
    
    public enemyDead OnEnemyDeath = delegate { };
    public EnemyHitted OnEnemyHitted = delegate { };
    public PlayerDamaged OnPlayerHitted = delegate { };

    
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


    private void Start()
    {
        OnPlayerHitted += TestLlamado;
    }

    public void EnemyDeath(Enemy _deadEnemy = null)
    {
        OnEnemyDeath?.Invoke(_deadEnemy);
    }

    public void enemyHitted(Enemy _hitted = null , float _damageDealt = 0.0f)
    {
        OnEnemyHitted?.Invoke(_hitted , _damageDealt);
    }

    private void TestLlamado(float dam, Enemy who = null)
    {
        // Esta funcion es para ver si se llama bien el delegate. Cambiala a gusto
        print("Se llamo luego de recibir damage");
    }

}
