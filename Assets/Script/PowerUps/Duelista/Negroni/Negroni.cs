using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class Negroni : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    MeleWeapon weapon;
    PlayerMaster player;
    [SerializeField] private List<Card> cards;


    private void Start()
    {
        manager = BuffManager.Instance;
        player = GameManager.Instance.GetPlayer();
        weapon = player.GetComponentInChildren<MeleWeapon>();
    }

    public override void Use()
    {
        CardSelectionUI.Instance.Show(cards, index =>
        {
            if (index == 0) WeaponBuff();
            else BodyBuff();
            Destroy(gameObject);
        });
    }
    void WeaponBuff()
    {
        BuffManager.Instance.AddBuffOnCriticalHit(new NegroniWeaponBuff(data));
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnHit(new NegroniBodyBuff(data, weapon));
    }
    private IEnumerator DrinkTime()
    {
        _hand.SetAnimationTrigger("Drink");
        yield return new WaitForSeconds(_useTime);
        if (_ReplaceItem)
        {
            Item Replace = Instantiate(_ReplaceItem);
            _inventory.ReplaceItem(this, Replace);


        }

        Destroy(gameObject);
    }
}
