using UnityEngine;

public class GinTonic : Item
{
    [SerializeField] BuffManager buffManager;
    [SerializeField] BuffData buffData;
    public override void Use()
    {
        buffManager.AddBuffOnHit(new SlowBuff(buffData));
        Destroy(gameObject);
    }


}
