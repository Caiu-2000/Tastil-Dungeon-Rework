using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class DryMartini : Item
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
        BuffManager.Instance.AddBuffOnCriticalHit(new DryMartiniWB(data, player));
    }
    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnParry(new DryMartiniBB(weapon, data));
    }

}
