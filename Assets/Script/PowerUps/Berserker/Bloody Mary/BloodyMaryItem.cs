using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BloodyMaryItem : Item
{
    PlayerMaster player;
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] private List<Card> cards;


    private void Start()
    {
        manager = BuffManager.Instance;
        player = GameManager.Instance.GetPlayer();
    }


    public override void Use()
    {
        CardSelectionUI.Instance.Show(cards, index =>
        {
            if (index == 0) WeaponBuff();
            else BodyBuff();
            Destroy(gameObject);
        });
    }
    void WeaponBuff()
    {
        BuffManager.Instance.AddBuffOnCriticalHit(new BMWB(data, player));
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnEnemyDeath(new BMBB(data, player));
    }
}
