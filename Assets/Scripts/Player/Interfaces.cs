using UnityEngine;

public interface IOnHitBuff
{
    void ExecuteOnHit(GameObject enemy, BuffManager manager);   //listo
}

public interface IOnAttackBuff
{
    void ExecuteOnAttack(GameObject player, BuffManager manager);   //listo
}

public interface IPassiveBuff
{
    void ExecuteOnPassive(GameObject player, BuffManager manager); //listo
}
public interface IOnEnemyDeath
{
    void ExecuteOnEnemyDeath(GameObject deadEnemy, BuffManager manager);   //listo
}
public interface IOnCriticalHit
{
    void ExecuteOnCriticalHit(GameObject enemy, BuffManager manager);   //Listo
}
public interface IOnPlayerHitted
{
    void ExecuteOnPlayerHitted(GameObject enemy, BuffManager manager); //listo
}
public interface IOnParry
{
    void ExecuteOnParry(BuffManager manager);   
}
public interface IProjectile
{
    void SetTarget(Transform target);
}