using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class LightInABottle : Item
{
    [SerializeField] BuffData data;
    PlayerMaster player;
    MeleWeapon weapon;
    Transform vfx;
    public void Start()
    {
        player = GameManager.Instance.GetPlayer();
        
    }
    public override void Use()
    {
        vfx = BuffManager.Instance.FindDeepChild(player.transform, "Electricity");
        BuffManager.Instance.AddBuffOnHit(new LightingArc(vfx));
        Destroy(gameObject);
    }
}
