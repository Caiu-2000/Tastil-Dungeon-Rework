using UnityEngine;

public abstract class WeaponHability : MonoBehaviour
{

    protected Entity _entity;



    public virtual void InitialiceHability(Entity parent , Weapon weapon)
    {

    }

    public virtual void RunHability() { }

}