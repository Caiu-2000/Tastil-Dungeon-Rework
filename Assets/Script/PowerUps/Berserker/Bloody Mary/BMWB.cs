using UnityEngine;

public class BMWB : IOnCriticalHit
{
    BuffData data;
    PlayerMaster player;
    public BMWB(BuffData data, PlayerMaster player)
    {
        this.data = data;
        this.player = player;
    }
    public void ExecuteOnCriticalHit(GameObject enemy, BuffManager manager)
    {
        player.Heal(player.GetMaxLife() * 0.2f+ 10f);
        manager.SpawnScreenVfx("Heal", 1f);
    }
}
