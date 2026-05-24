using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Fireball : Item
{
    PlayerMaster player;
    MeleWeapon weapon;
    RangedWeapon bow;
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    bool isSelected;
    private void Update()
    {
        if (imagen.IsActive() && isSelected)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
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
        manager.AddBuffOnHit(new FWB(data, weapon));
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    void BodyBuff()
    {
        manager.AddBuffOnPlayerHitted(new FBB(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
