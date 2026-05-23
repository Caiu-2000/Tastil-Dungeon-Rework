using UnityEngine;

public class FBB : IOnPlayerHitted
{
    BuffData data;
    PlayerMaster player;
    public FBB (BuffData data, PlayerMaster player)
    {
        this.data = data;
        this.player = player;
    }

    public void ExecuteOnPlayerHitted(GameObject enemy, BuffManager manager)
    {
        var vfx = manager.SpawnAndReturn("RingOfFire", player.transform.position, player.transform);
        vfx.gameObject.transform.SetParent(player.transform);
        vfx.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y/2, player.transform.position.z);
    }
}
