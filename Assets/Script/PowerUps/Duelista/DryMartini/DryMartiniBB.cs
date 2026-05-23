using UnityEngine;

public class DryMartiniBB : IOnParry
{
    MeleWeapon weapon;
    BuffData data;
    public DryMartiniBB(MeleWeapon weapon, BuffData data)
    {
        this.weapon = weapon;
        this.data = data;
    }
    public void ExecuteOnParry(BuffManager manager)
    {
        weapon.GuaranteeCriticalHit();
    }
}
