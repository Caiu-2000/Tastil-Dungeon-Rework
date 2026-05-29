using UnityEngine;

public class ParryCollision : MonoBehaviour , IParryable
{
    public Enemy ParentEnemy;

    public float HitDuration;


    public void ChangeDuration(float duration)
    {
        Destroy(this, duration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1.5f, 1.5f, 1.5f));
    }

    public void Parry()
    {
        print("Parreado");
        ParentEnemy.ApplyParry();
    }
}
