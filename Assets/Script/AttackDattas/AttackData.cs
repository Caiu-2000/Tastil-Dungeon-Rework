using System;

[Serializable]
public class AttackData
{
    public float CollisionTime;
    public float AttackDuration;
    public float Damagemultiplier = 1.0f;
    public float AttackCD;
    public float KnockbackForce;
    public bool Parriable;
    public float ParryStart;
    public float ParryWindow;
}
