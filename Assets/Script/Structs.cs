
using UnityEngine;

public struct hittData
{
    public float Damage;
    public float KnockbackForce ;
    public Vector3 KnockbackFrom ;

    public hittData(float damage,Vector3 from, float knockbackforce = 1)
    {
        Damage = damage;
        KnockbackFrom = from;
        KnockbackForce = knockbackforce;

    }
}