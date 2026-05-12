using Unity.VisualScripting;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] private float _healingPower = 10.0f;
    
    public override void Use()
    {
        base.Use();
        GameManager.Instance.GetPlayer().Heal(_healingPower);
        Destroy(gameObject);
    }




}
