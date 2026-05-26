using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Absenta : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    MeleWeapon weapon;
    PlayerMaster player;
    bool isActive;

    private void Update()
    {
        if (imagen.IsActive() && isActive)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                WeaponBuff();
            if (Keyboard.current.yKey.wasPressedThisFrame)
                BodyBuff();
        }

    }
    public override void Use()
    {
        player = gameObject.transform.root.GetComponent<PlayerMaster>();
        weapon = player.GetComponentInChildren<MeleWeapon>();
        imagen.gameObject.SetActive(true);
        isActive = true;
    }
    void WeaponBuff()
    {
        manager.AddBuffOnHit(new AbsentaWeaponBuff(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void BodyBuff()
    {
        manager.AddBuffOnPlayerHitted(new AbsentaBodyBuff(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
