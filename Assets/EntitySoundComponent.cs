
using UnityEngine;

[System.Serializable]
public class EntitySoundComponent : SoundEmitterComponent
{
    protected Entity parentEntity;


    public override void InitializeThis(Entity ParentEntity= null)
    {
        ParentEntity.OnEntityAttacked += PlayAttack;
        ParentEntity.OnEntityDead += PlayDeath;
        ParentEntity.OnDamaged += PlayDamaged;
   
        base.InitializeThis(ParentEntity);
        
        
    }




    public void PlayDamaged()
    {
        Debug.Log("Damaged");
    }
    public void PlayDeath()
    {
        Debug.Log("dead");
    }
    public void PlayAttack()
    {
        Debug.Log("Attacked");
    }
}
