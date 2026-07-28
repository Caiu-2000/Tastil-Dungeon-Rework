using UnityEngine;

public class DryMartiniWB : IOnCriticalHit
{
    BuffData data;
    PlayerMaster player;

    public DryMartiniWB (BuffData data, PlayerMaster player)
    {
        this.data = data; 
        this.player = player;
    }

    public void ExecuteOnCriticalHit(GameObject enemy, BuffManager manager)
    {
        if (enemy.TryGetComponent<MovementComponent>(out MovementComponent movecomp))
        {
            movecomp.ApplyKnockback(enemy.transform.position - player.transform.position, 1000);
        }
            manager.AddBuffOnAttack(new DryMartiniDashWB(enemy.transform));
    }
}
