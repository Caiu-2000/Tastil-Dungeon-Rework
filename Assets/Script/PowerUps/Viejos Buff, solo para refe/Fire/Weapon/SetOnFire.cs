using UnityEngine;

public class SetOnFire : IOnHitBuff
{
    BuffData data;
    public SetOnFire(BuffData data)
    {
        this.data = data;
    }
    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        manager.SpawnProjectile("Fire", enemy.transform.position, enemy.transform);
    }
}
