using UnityEngine;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
public class SlowBuff : IOnHitBuff
{
    BuffData data;
    int stacks = 0;
    Coroutine activeCoroutine;
    GameObject activeVFX;
    [SerializeField]GameObject iceVFX;
    public SlowBuff(BuffData data)
    {
        this.data = data;
    }
    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        if (activeVFX != iceVFX || activeVFX == null)
        {
            MovementComponent mc = enemy.GetComponent<MovementComponent>();
            if (mc == null) return;

            stacks++;

            // cancel existing timer and restart it (refresh)
            if (activeCoroutine != null)
            {
                manager.StopCoroutineExternal(activeCoroutine);
                if (activeVFX != null) Object.Destroy(activeVFX);
            }

            if (stacks >= 5)
            {
                stacks = 0;
                activeVFX = manager.SpawnAndReturn("StunVFX", enemy.transform.position, enemy.transform);
                iceVFX = activeVFX;
                activeVFX.GetComponent<IcePrisonVFX>().Target = enemy.gameObject.transform;
                activeVFX.GetComponent<IcePrisonVFX>().Play();
                activeCoroutine = manager.StartCoroutineExternal(ApplyStun(mc, data.duration));

            }
            else
            {
                activeCoroutine = manager.StartCoroutineExternal(ApplySlow(mc, data.duration, manager));
            }
        }
    }
    private IEnumerator ApplySlow(MovementComponent mc, float duration, BuffManager manager)
    {
        float originalSpeed = mc.GetSpeed();
        mc.SetSpeed(originalSpeed * 0.5f);
        activeVFX = manager.SpawnAndReturn("SlowVFX", mc.transform.position, mc.transform);
        activeVFX.GetComponent<SlowSnowVFX>().Target = mc.gameObject.transform;
        yield return new WaitForSeconds(duration);
        mc.SetSpeed(originalSpeed);
        if (activeVFX != null) Object.Destroy(activeVFX);
    }
    private IEnumerator ApplyStun(MovementComponent mc, float duration)
    {
        mc.SetCanWalk(false);
        yield return new WaitForSeconds(duration);
        mc.SetCanWalk(true);
        Object.Destroy(activeVFX);
        activeVFX = null;
    }
}