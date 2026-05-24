using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FourHorsemanitem : Item
{
    PlayerMaster player;
    MeleWeapon weapon;
    RangedWeapon bow;
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    [SerializeField] VFXController controller;
    bool isSelected;
    private void Update()
    {
        if(imagen.IsActive()&& isSelected)
        {if (Keyboard.current.tKey.wasPressedThisFrame)
                this.WeaponBuff();
            if (Keyboard.current.yKey.wasPressedThisFrame)
                this.BodyBuff();
        }

    }
    public override void Use()
    {
        player = gameObject.transform.root.GetComponent<PlayerMaster>();
        weapon = player.GetComponentInChildren<MeleWeapon>();
        bow = player.GetComponent<RangedWeapon>();
        imagen.gameObject.SetActive(true);
        isSelected = true;
    }
    void WeaponBuff()
    {
        manager.AddBuffOnAttack(new FHBW(data, player, weapon));
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    void BodyBuff()
    {
        var buff = new FHBB(data, player, weapon, bow);
        manager.AddBuffOnAttack(buff);
        manager.AddBuffPassive(buff, 0.1f);
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
