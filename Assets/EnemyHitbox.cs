using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IHittable
{
    [SerializeField]
    private Enemy ParentEnemy;

    public void Hit(float damage = 0, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        ParentEnemy.applyDamage(damage, ApplyKnockback, knockbackForce , KnockBackFrom);
    }
}
