using UnityEngine;

public class B52WB : IOnCriticalHit
{
    BuffData data;
    public B52WB(BuffData data)
    {
        this.data = data;
    }
    public void ExecuteOnCriticalHit(GameObject enemy, BuffManager manager)
    {
        manager.SpawnProjectile("ExplosionSmall", enemy.transform.position, enemy.transform);
    }
}