using UnityEngine;

public class ThunderBuff : IOnHitBuff
{
    BuffData data;
    int stack = 0;
    public ThunderBuff(BuffData data)
    {
        this.data = data;
    }

    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        stack++;
        if (stack >= 3)
        {
            Collider[] collisions = Physics.OverlapSphere(enemy.transform.position, data.radius);
            foreach (Collider collider in collisions)
            {
                if (collider.gameObject.CompareTag("Enemy") && collider.gameObject != enemy)
                    manager.SpawnProjectile("ElectroBall", enemy.transform.position, collider.transform);
            }
        }
    }
}
