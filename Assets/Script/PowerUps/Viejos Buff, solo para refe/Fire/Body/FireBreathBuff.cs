using UnityEngine;

public class FireBreathBuff : IPassiveBuff
{
    BuffData data;
    
    public FireBreathBuff(BuffData data)
    {
        this.data = data;
    }
    public void ExecuteOnPassive(GameObject player, BuffManager manager)
    {
        manager.SpawnProjectile("FireBreath", player.transform.position, player.transform);
    }
}
