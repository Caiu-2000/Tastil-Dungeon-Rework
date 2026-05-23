using System.Collections;
using UnityEngine;

public class NegroniWeaponBuff : IOnCriticalHit
{
    BuffData data;
    public NegroniWeaponBuff(BuffData data)
    {
        this.data = data;
    }

    public void ExecuteOnCriticalHit(GameObject enemy, BuffManager manager)
    {
        manager.StartCoroutineExternal(ApplyBleed(enemy, manager));
    }
    private IEnumerator ApplyBleed(GameObject enemy, BuffManager manager)
    {
        GameObject vfx = manager.SpawnAndReturn("Bleed", enemy.transform.position, enemy.transform);
        float elapsed = 0f;
        float tickRate = 0.5f;
        while (elapsed < data.duration && enemy != null)
        {
            yield return new WaitForSeconds(tickRate);
            elapsed += tickRate;
            enemy.GetComponent<Entity>().applyDamage(data.damage * tickRate);
        }

        if (vfx != null) Object.Destroy(vfx);
    }
}
