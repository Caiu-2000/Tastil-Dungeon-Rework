using UnityEngine;

public class DryMartiniBB : IOnParry
{
    Weapon weapon;
    BuffData data;
    public DryMartiniBB(Weapon weapon, BuffData data)
    {
        this.weapon = weapon;
        this.data = data;
    }
    public void ExecuteOnParry(BuffManager manager)
    {
        weapon.GuaranteeCriticalHit();
    }
}
