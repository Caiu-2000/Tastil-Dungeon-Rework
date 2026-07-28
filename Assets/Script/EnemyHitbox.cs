using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IHittable
{
    [SerializeField]
    private Enemy ParentEnemy;

    public void Hit(HittData hitt)
    {
        ParentEnemy.applyDamage(hitt);
    }
}
