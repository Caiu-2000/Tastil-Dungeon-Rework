using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    #region singletone
    public static BuffManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else Destroy(gameObject);
        projectileRegistry.Init();

    }
    #endregion
    #region Listas
    //Listas para almacenar que buffos estan activos
    private List<IOnHitBuff> onHitBuffs = new List<IOnHitBuff>();
    private List<IOnAttackBuff> onAttackBuffs = new List<IOnAttackBuff>();
    private List<IPassiveBuff> passiveBuffs = new List<IPassiveBuff>();
    private List<IOnEnemyDeath> enemyDeathBuffs = new List<IOnEnemyDeath>();
    private List<IOnCriticalHit> onCriticalHits = new List<IOnCriticalHit>();
    private List<IOnPlayerHitted> onPlayerHitteds = new List<IOnPlayerHitted>();
    private List<IOnParry> onParries = new List<IOnParry>();
    #endregion
    #region add/remove buffs
    //"metodos" para agregar buffos
    public void AddBuffOnHit(IOnHitBuff buff) => onHitBuffs.Add(buff);
    public void AddBuffOnAttack(IOnAttackBuff buff) => onAttackBuffs.Add(buff);
    public void AddBuffPassive(IPassiveBuff buff, float tickInterval = 0)
    {
        passiveBuffs.Add(buff);
        StartCoroutine(TickPassive(buff, tickInterval));
    }
    public void AddBuffOnEnemyDeath(IOnEnemyDeath buff) => enemyDeathBuffs.Add(buff);
    public void AddBuffOnCriticalHit(IOnCriticalHit buff) => onCriticalHits.Add(buff);
    public void AddBuffOnPlayerHitted(IOnPlayerHitted buff) => onPlayerHitteds.Add(buff);
    public void AddBuffOnParry(IOnParry buff) => onParries.Add(buff);
    //"metodos" para remover buffos
    public void RemoveBuffOnHit(IOnHitBuff buff) => onHitBuffs.Remove(buff);
    public void RemoveBuffOnAttack(IOnAttackBuff buff) => onAttackBuffs.Remove(buff);
    public void RemoveBuffPassive(IPassiveBuff buff) => passiveBuffs.Remove(buff);
    public void RemoveBuffOnEnemyDeath(IOnEnemyDeath buff) => enemyDeathBuffs.Remove(buff);
    public void RemoveBuffOnCriticalHit(IOnCriticalHit buff) => onCriticalHits.Remove(buff);
    public void RemoveBuffOnPlayerHitted(IOnPlayerHitted buff) => onPlayerHitteds.Remove(buff);
    public void RemoveBuffOnParry(IOnParry buff) => onParries.Remove(buff);
    #endregion
    [SerializeField] ProjectileRegistry projectileRegistry;
    [SerializeField] VFXController controller;
    #region Triggers
    public void TriggerOnHit(GameObject enemy)
    {
        foreach (var buff in onHitBuffs) buff.ExecuteOnHit(enemy, this);
    }
    public void TriggerOnAttack(GameObject player)
    {
        foreach (var buff in new List<IOnAttackBuff>(onAttackBuffs))
            buff.ExecuteOnAttack(player, this);
    }
    public void TriggerOnEnemyDeath(GameObject deadEnemy)
    {
        foreach (var buff in enemyDeathBuffs) buff.ExecuteOnEnemyDeath(deadEnemy, this);
    }
    public void TriggerOnCriticalHit(GameObject enemy)
    {
        foreach (var buff in onCriticalHits) buff.ExecuteOnCriticalHit(enemy, this);
    }
    public void TriggerOnPlayerHitted(GameObject enemy)
    {
        foreach(var buff in onPlayerHitteds) buff.ExecuteOnPlayerHitted(enemy, this);
    }
    public void TriggerOnParry()
    {
        foreach(var buff in onParries) buff.ExecuteOnParry(this);
    }
    #endregion
    #region helper Methods
    private IEnumerator TickPassive(IPassiveBuff buff, float interval)
    {
        while (passiveBuffs.Contains(buff))
        {
            yield return new WaitForSeconds(interval);
            buff.ExecuteOnPassive(gameObject, this);
        }
    }
    public void SpawnProjectile(string key, Vector3 pos, Transform target)
    {
        var obj = Instantiate(projectileRegistry.Get(key), pos, Quaternion.identity);
        //obj.GetComponent<IProjectile>().SetTarget(target); 
    }
    public void SpawnScreenVfx(string key, float intesnity)
    {
        switch(key)
        {
            case "Berserker bloodlust":
                controller.ShowLowHealth(intesnity);
                break;
            case "Heal":
                controller.ShowHeal();
                break;
            case "dash":
                controller.ShowDash();
                break;
        }
    }
    public void HideScreenVfx()
    {
        controller.HideVFX();
    }    
    public GameObject SpawnAndReturn(string key, Vector3 pos, Transform target)
    {
        var obj = Instantiate(projectileRegistry.Get(key), pos, Quaternion.identity);
        obj.transform.SetParent(target); // sticks to the enemy as it moves
        return obj;
    }
    public Coroutine StartCoroutineExternal(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
    public void StopCoroutineExternal(Coroutine routine)
    {
        StopCoroutine(routine);
    }
    #endregion
    public BuffManager GetManager()
    {
        return BuffManager.Instance;
    }
}

