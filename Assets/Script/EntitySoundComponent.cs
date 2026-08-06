
using Unity.VisualScripting.FullSerializer;
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

    public void PlayDamaged(hittData? data = null)
    {
       
        PlaySound(SoundTypes.Damaged , true);
    }
    public void PlayDeath()
    {
        PlaySound(SoundTypes.Death);
    }
    public void PlayAttack()
    {
        PlaySound(SoundTypes.Hit);
    }
}
