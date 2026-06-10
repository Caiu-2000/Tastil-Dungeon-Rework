using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class B52Item : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    PlayerMaster player;
    bool isActive;

    private void Start()
    {
        manager = BuffManager.Instance;
        player = GameManager.Instance.GetPlayer();
        imagen = player.transform.Find("PlayerUI/BuffSelector").GetComponent<Image>();
    }
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
        BuffManager.Instance.AddBuffOnCriticalHit(new B52WB(data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnEnemyDeath(new B52BB(data));
        imagen.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
