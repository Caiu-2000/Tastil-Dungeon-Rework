using System.Collections;
using UnityEngine;

public class DryMartiniDashWB : IOnAttackBuff
{
    Transform target;
    BuffManager manager;
    public DryMartiniDashWB(Transform target)
    {
        this.target = target;
    }
    public void ExecuteOnAttack(GameObject player, BuffManager manager)
    {
        this.manager = manager;
        if (target != null)
            manager.StartCoroutineExternal(DashTowards(player, target.position));

        manager.RemoveBuffOnAttack(this);
    }

    private IEnumerator DashTowards(GameObject player, Vector3 targetPos)
    {
        float dashSpeed = 60f;
        float threshold = 1f;
        manager.SpawnScreenVfx("dash", 1f);
        while (Vector3.Distance(player.transform.position, targetPos) > threshold)
        {
            
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                targetPos,
                dashSpeed * Time.deltaTime
            );
            yield return null; 
        }
    }
}
