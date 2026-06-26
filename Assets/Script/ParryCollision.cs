using UnityEngine;
using System.Collections;

public class ParryCollision : MonoBehaviour , IParryable
{
    public Enemy ParentEnemy;

    public float HitDuration;


    public void ChangeDuration(float duration)
    {
        if (duration == -2.0f)
        {
            StartCoroutine(DestroyNextFrame());
            return;
        }
        Destroy(this.gameObject, duration);
    }

    private void OnDrawGizmos()
    {
        if (!GameManager.Instance.DebugActive) { return; }
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1.5f, 1.5f, 1.5f));
    }

    public void Parry()
    {
    
        ParentEnemy.ApplyParry();
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return null;
        Destroy(this.gameObject);
    }

}
