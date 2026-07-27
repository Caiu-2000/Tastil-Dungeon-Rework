


using UnityEngine;

public struct Hitt
{
    public float HittDamage ;
    public float knockback;
    public Vector3 AttackFrom;
    public Hitt(float damage, Vector3 attackFrom = default , float knock = 0 )
    {
        HittDamage = damage;
        knockback = knock;
        AttackFrom = attackFrom;
    }

}
