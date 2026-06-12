using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Fireball : Item
{
    PlayerMaster player;
    MeleWeapon weapon;
    RangedWeapon bow;
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] private List<Card> cards;


    private void Start()
    {
        manager = BuffManager.Instance;
        player = GameManager.Instance.GetPlayer();
        weapon = player.GetComponentInChildren<MeleWeapon>();
        bow = player.GetComponent<RangedWeapon>();

    }


    public override void Use()
    {
        weapon = player.GetComponentInChildren<MeleWeapon>();

        CardSelectionUI.Instance.Show(cards, index =>
        {
            if (index == 0) WeaponBuff();
            else BodyBuff();
            Destroy(gameObject);
        });

    }
    void WeaponBuff()
    {
        BuffManager.Instance.AddBuffOnHit(new FWB(data, weapon));
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnPlayerHitted(new FBB(data, player));
    }
}
