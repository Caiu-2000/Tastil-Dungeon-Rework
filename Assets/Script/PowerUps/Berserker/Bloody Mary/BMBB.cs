using UnityEngine;

public class BMBB : IOnEnemyDeath
{
    BuffData data;
    PlayerMaster player;
    public BMBB (BuffData data, PlayerMaster player)
    {
        this.data = data;
        this.player = player;
    }
    public void ExecuteOnEnemyDeath(GameObject deadEnemy, BuffManager manager)
    {
        //player.Heal(player.GetMaxLife() * 0.33f);
        Debug.Log(deadEnemy.gameObject);
        Vector3 pos = deadEnemy.transform.root.position;
        Transform test = deadEnemy.transform.root;
        manager.SpawnProjectile("HealOrb", pos, test);
        
    }
}
