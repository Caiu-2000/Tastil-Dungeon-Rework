using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class B52Item : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager;
    [SerializeField] Image imagen;
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
        imagen.gameObject.SetActive(true);
        isActive = true;
    }
    void WeaponBuff()
    {
        manager.AddBuffOnCriticalHit(new B52WB(data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void BodyBuff()
    {
        manager.AddBuffOnEnemyDeath(new B52BB(data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
