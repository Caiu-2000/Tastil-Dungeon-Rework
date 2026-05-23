using UnityEngine;

public class ArrowBottleBuff : IOnAttackBuff
{
    int stack = 0;
    BuffData data;
    GameObject player;
    public ArrowBottleBuff (BuffData data, GameObject player)
    {
        this.data = data;
        this.player = player;
    }
    public void ExecuteOnAttack(GameObject player, BuffManager manager)
    {
        stack++;
        if(stack >= 3)
        {
            manager.SpawnProjectile("Arrow", player.transform.position, player.transform);
            stack = 0;
        }
        
    }
}
