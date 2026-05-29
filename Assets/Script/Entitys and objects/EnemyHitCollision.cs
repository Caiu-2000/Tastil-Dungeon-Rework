using System;
using UnityEngine;

[Serializable]
public class EnemyHitCollision : MonoBehaviour
{
    [SerializeField] public float HitDuration = 0.2f;
    [SerializeField] public Enemy parentEnemy;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerMaster>())
        {
            parentEnemy.HitConnectded(other);
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        if(HitDuration == -1.0f) return;
        Destroy(this , HitDuration);    
    }

    public void ChangeDuration(float duration)
    {
        Destroy(this, duration);
    }
    private void OnDrawGizmos()
    {
        if (!GameManager.Instance.DebugActive) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1f, 1f, 1f));
    }


}
