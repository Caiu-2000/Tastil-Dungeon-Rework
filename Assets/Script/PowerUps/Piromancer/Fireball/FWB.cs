using UnityEngine;

public class FWB : IOnHitBuff
{
    int stacks = 0;
    BuffData data;
    Weapon weapon;
    Transform firesword;
    public FWB (BuffData data, Weapon weapon)
    {
        this.data = data;
        this.weapon = weapon;
        Debug.Log (weapon);
        firesword = weapon.transform.Find("Fire Weapon");
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
