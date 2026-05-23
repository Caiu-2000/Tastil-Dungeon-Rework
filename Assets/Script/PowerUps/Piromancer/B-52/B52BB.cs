using UnityEngine;

public class B52BB : IOnEnemyDeath
{
    BuffData data;
    public B52BB(BuffData data)
    {
        this.data = data;
    }
    public void ExecuteOnEnemyDeath(GameObject deadEnemy, BuffManager manager)
    {
        manager.SpawnProjectile("Explosion", deadEnemy.transform.position, deadEnemy.transform);
    }
}