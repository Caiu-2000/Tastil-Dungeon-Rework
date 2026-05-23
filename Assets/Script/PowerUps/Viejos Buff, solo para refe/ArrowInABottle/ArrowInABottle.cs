using UnityEngine;

public class ArrowInABottle : Item
{
    [SerializeField] BuffManager buffManager;
    [SerializeField] BuffData buffData;
    public override void Use()
    {
        buffManager.AddBuffOnAttack(new ArrowBottleBuff(buffData, this._inventory.gameObject));
        Destroy(gameObject);
    }


}
