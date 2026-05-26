using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FHBB : IOnAttackBuff, IPassiveBuff
{
    BuffData data;
    PlayerMaster player;
    MeleWeapon weapon;
    RangedWeapon bow;
    float currentLife;
    float maxLife;
    float deltaLife;
    public FHBB (BuffData data, PlayerMaster player, MeleWeapon weapon, RangedWeapon bow)
    {
        this.data = data;
        this.player = player;
        this.weapon = weapon;
        this.bow = bow;
        maxLife = player.GetMaxLife();
        currentLife = player.GetLife();
    }
    public void ExecuteOnAttack(GameObject player, BuffManager manager)
    {
        currentLife = this.player.GetLife();
        deltaLife = Mathf.Clamp(maxLife - currentLife, 0, 70);
        float powerOfLife = Mathf.Pow(deltaLife, 2);
        float damageMultiplier = Mathf.Clamp(((2* powerOfLife)/ 49 + 100)/100, 1, 3);
        weapon.SetDamage(damageMultiplier);
    }

    public void ExecuteOnPassive(GameObject player, BuffManager manager)
    {
        currentLife = this.player.GetLife();
        deltaLife = Mathf.Clamp(maxLife - currentLife, 0, 70);
        Debug.Log(deltaLife);
        float intensity = Mathf.Lerp(0f, 2f, deltaLife / 70f);
        Debug.Log(intensity);
        if (deltaLife > 0)
            manager.SpawnScreenVfx("Berserker bloodlust", intensity);
        else
            manager.HideScreenVfx();

    }
}
