using UnityEngine;
using System.Collections;

public class AbsentaWeaponBuff : IOnHitBuff
{
    BuffData data;
    PlayerMaster player;
    int stacks = 0;
    bool healOnCooldown = false;
    Coroutine activeCoroutine;

    public AbsentaWeaponBuff(BuffData data, PlayerMaster player)
    {
        this.data = data;
        this.player = player;
    }

    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        if (activeCoroutine != null)
            manager.StopCoroutineExternal(activeCoroutine);
        if (stacks < 10)
        {
            stacks++;
            player.AddArmor(data.armor);
        }
        if (stacks >= 10 && !healOnCooldown)
        {
            healOnCooldown = true;
            player.Heal(player.GetMaxLife() * data.heal); 
            manager.StartCoroutineExternal(HealCooldown());
        }
        activeCoroutine = manager.StartCoroutineExternal(RemoveArmor(manager));
    }
    private IEnumerator RemoveArmor(BuffManager manager)
    {
        yield return new WaitForSeconds(data.duration); // duration = armor duration
        stacks = 0;
        player.RemoveArmor(data.radius * 10); 
    }
    private IEnumerator HealCooldown()
    {
        yield return new WaitForSeconds(data.tickInterval);
        healOnCooldown = false;
    }
}