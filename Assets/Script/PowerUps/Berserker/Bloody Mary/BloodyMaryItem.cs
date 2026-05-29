using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BloodyMaryItem : Item
{
    PlayerMaster player;
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] Image imagen;
    bool isSelected;
    private void Update()
    {
        if (imagen.IsActive()&&isSelected)
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
        imagen.gameObject.SetActive(true);
        isSelected = true;
    }
    void WeaponBuff()
    {
        BuffManager.Instance.AddBuffOnCriticalHit(new BMWB(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnEnemyDeath(new BMBB(data, player));
        imagen.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
