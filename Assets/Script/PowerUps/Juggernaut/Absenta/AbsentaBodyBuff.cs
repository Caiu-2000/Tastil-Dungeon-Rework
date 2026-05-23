using UnityEngine;
using System.Collections;

public class AbsentaBodyBuff : IOnPlayerHitted
{
    BuffData data;
    PlayerMaster player;
    bool shieldUsed = false;
    public AbsentaBodyBuff(BuffData data, PlayerMaster player)
    {
        this.data = data;
        this.player = player;
        player.AddMaxLife(player.GetMaxLife() * data.radius);    // reusing radius as % bonus
        player.AddArmor(player.GetMaxLife() * data.damage);      // reusing damage as % reduction
    }

    public void ExecuteOnPlayerHitted(GameObject enemy, BuffManager manager)
    {
        if (shieldUsed) return;
        if (player.GetLife() / player.GetMaxLife() < 0.25f)
        {
            shieldUsed = true;
            player.SetShield(player.GetMaxLife() * 0.3f);
            manager.StartCoroutineExternal(ShieldCooldown());
        }
    }

    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(data.durationTwo); // duration = cooldown
        shieldUsed = false;
    }
}