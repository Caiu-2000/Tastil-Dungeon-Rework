using UnityEngine;

public interface IParryable
{
    void Parry();

}


public interface IBreackable
{
    void Breack();
}

public interface IHittable
{
    void Hit(float damage = 0, bool ApplyKnockback = false, float knockbackForce = 0.0f, Transform KnockBackFrom = null);
}