using UnityEngine;

public class PerkManager : MonoBehaviour
{

    //EN ESTE SCRIPT LA NOMENCLATURA VA A DICIEMBRE



    public static PerkManager Instance { get; private set; }
 
    public delegate void enemyDead(Enemy dead); 
    public delegate void EnemyHitted(Enemy hitted = null, float Damage = 0.0f);
    
    
    public enemyDead OnEnemyDeath = delegate { };
    public EnemyHitted OnEnemyHitted = delegate { };

    
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

    public void EnemyDeath(Enemy _deadEnemy = null)
    {
        OnEnemyDeath?.Invoke(_deadEnemy);
    }

    public void enemyHitted(Enemy _hitted = null , float _damageDealt = 0.0f)
    {
        OnEnemyHitted?.Invoke(_hitted , _damageDealt);
    }

}
