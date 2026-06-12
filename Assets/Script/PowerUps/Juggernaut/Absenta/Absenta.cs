using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Absenta : Item
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
        BuffManager.Instance.AddBuffOnHit(new AbsentaWeaponBuff(data, player));
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnPlayerHitted(new AbsentaBodyBuff(data, player));
    }
}
