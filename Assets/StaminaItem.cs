using System.Collections;

using UnityEngine;

public class StaminaItem : Item
{
    [SerializeField] private float _StamRecuperation = 25.0f;

    public override void Use()
    {
        StartCoroutine(UseItem("Drink"));

    }
    protected virtual IEnumerator UseItem(string AnimName = "")
    {
        if (AnimName != "") _hand.SetAnimationTrigger("Drink");
        yield return new WaitForSeconds(_useTime);
        if (_ReplaceItem)
        {
            Item Replace = Instantiate(_ReplaceItem);
            _inventory.ReplaceItem(this, Replace);


        }
        GameManager.Instance.GetPlayer().AddStamina(_StamRecuperation);
        Destroy(gameObject);
    }
}
