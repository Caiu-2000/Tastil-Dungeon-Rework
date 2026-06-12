using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class B52Item : Item
{
    [SerializeField] BuffData data;
    [SerializeField] BuffManager manager = BuffManager.Instance;
    [SerializeField] private List<Card> cards;
    PlayerMaster player;

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
        BuffManager.Instance.AddBuffOnCriticalHit(new B52WB(data));
    }

    void BodyBuff()
    {
        BuffManager.Instance.AddBuffOnEnemyDeath(new B52BB(data));
    }
}
