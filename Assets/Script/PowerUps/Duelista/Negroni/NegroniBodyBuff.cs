using UnityEngine;

public class NegroniBodyBuff : IOnHitBuff
{
    BuffData data;
    GameObject enemyCache;
    Weapon weapon;
    
    public NegroniBodyBuff(BuffData data, Weapon weapon)
    {
        this.data = data;
        this.weapon = weapon;
    }
    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        if(enemyCache != null || enemy != enemyCache)
        {
            enemyCache = enemy;
            weapon.ResetCritChance();
        }
        else
        {
            weapon.AddCritChance(5);
        }
    }
}
