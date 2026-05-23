using Unity.VisualScripting;
using UnityEngine;

public class LightingBuff : Item
{
    [SerializeField]BuffManager buffManager;
    [SerializeField] BuffData buffData;   
    public override void Use()
    {
        buffManager.AddBuffOnHit(new ThunderBuff(buffData));
        Destroy(gameObject);
    }

    
}
