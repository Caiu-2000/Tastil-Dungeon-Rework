using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DryMartini : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    MeleWeapon weapon;
    PlayerMaster player;
    bool isActive;
    private void Update()
    {
        if (imagen.IsActive()&& isActive)
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
        BuffManager.Instance.AddBuffOnCriticalHit(new DryMartiniWB(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnParry(new DryMartiniBB(weapon, data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
