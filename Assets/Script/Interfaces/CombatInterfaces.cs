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
    void Hit(HittData hitt);
}