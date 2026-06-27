using System.Collections;

using UnityEngine;


public class HealingItem : Item
{
    [SerializeField] private float _healingPower = 10.0f;
    
    public override void Use()
    {
        StartCoroutine(UseItem("Drink"));
        
    }




    protected override IEnumerator UseItem(string AnimName = "")
    {
        if (AnimName != "") _hand.SetAnimationTrigger("Drink");
        yield return new WaitForSeconds(_useTime);
        if (_ReplaceItem)
        {
            Item Replace = Instantiate(_ReplaceItem);
            _inventory.ReplaceItem(this, Replace);


        }
        GameManager.Instance.GetPlayer().Heal(_healingPower);
        Destroy(gameObject);
    }
}
