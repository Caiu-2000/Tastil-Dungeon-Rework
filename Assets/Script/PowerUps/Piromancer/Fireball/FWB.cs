using UnityEngine;

public class FWB : IOnHitBuff
{
    int stacks = 0;
    BuffData data;
    MeleWeapon weapon;
    Transform firesword;
    public FWB (BuffData data, MeleWeapon weapon)
    {
        this.data = data;
        this.weapon = weapon;
        firesword = weapon.transform.Find("Fire Sword");
        Debug.Log(weapon);
    }

    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        stacks++;
        if (stacks == 2)
        {
            firesword.gameObject.SetActive(true);
        }
        if (stacks == 3)
        {
            stacks = 0;
            manager.SpawnProjectile("Fire", enemy.transform.position, enemy.transform);
            firesword.gameObject.SetActive(false);
        }
    }
}
