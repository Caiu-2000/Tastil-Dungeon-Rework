using UnityEngine;

public class NegroniBodyBuff : IOnHitBuff
{
    BuffData data;
    GameObject enemyCache;
    MeleWeapon weapon;
    
    public NegroniBodyBuff(BuffData data, MeleWeapon weapon)
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
