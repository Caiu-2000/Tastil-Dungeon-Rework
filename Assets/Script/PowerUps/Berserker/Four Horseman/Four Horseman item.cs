using UnityEngine;
using System.Collections.Generic;

public class FourHorsemanitem : Item
{
    [SerializeField] private PlayerMaster player;
    [SerializeField] private MeleWeapon weapon;
    [SerializeField] private BuffData data;
    [SerializeField] private List<Card> cards;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        weapon = player.GetComponentInChildren<MeleWeapon>();
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

    private void WeaponBuff()
    {   // ACA SAQUE LA VARIABLE DE WEAPON Y PUSE LA REFERENCIA
        BuffManager.Instance.AddBuffOnAttack(new FHBW(data, player, GameManager.Instance.CurrentWeapon));
    }

    private void BodyBuff()
    {
        var buff = new FHBB(data, player, weapon, player.GetComponent<RangedWeapon>());
        BuffManager.Instance.AddBuffOnAttack(buff);
        BuffManager.Instance.AddBuffPassive(buff, 0.1f);
    }
}