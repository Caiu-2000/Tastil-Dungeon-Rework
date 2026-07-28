

using UnityEngine;
public struct HittData
{
    public float Damage { get; }
    public float KnockbackForce {  get; }
    public Vector3 Hittfrom { get; }

    public Entity who;

    public HittData(float damage,Entity FromWHo = null, Vector3? hittfrom = null, float knoForce = 0)
    {
        Damage = damage;
        KnockbackForce = knoForce;
        Hittfrom = Vector3.zero;
        if (hittfrom.HasValue) Hittfrom = hittfrom.Value;
        who = FromWHo;
    }

}
