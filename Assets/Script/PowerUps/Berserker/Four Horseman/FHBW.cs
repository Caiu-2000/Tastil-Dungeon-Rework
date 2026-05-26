using UnityEngine;

public class FHBW : IOnAttackBuff
{
    BuffData data;
    PlayerMaster player;
    MeleWeapon weapon;
    Transform bloodsword;
    public FHBW(BuffData data, PlayerMaster player, MeleWeapon weapon)
    {
        this.data = data;
        this.player = player;
        this.weapon = weapon;
        bloodsword = weapon.transform.Find("Sword/HOJA1/bloodsword");
        bloodsword.gameObject.SetActive(true);

    }
    public void ExecuteOnAttack(GameObject player, BuffManager manager)
    {
        this.player.applyDamage(this.player.GetLife() * 0.05f);
        weapon.SetDamage(1.15f);
        //manager.SpawnProjectile("BloodSword", weapon.transform.position, weapon.transform);
    }
}
