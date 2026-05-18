using System.Collections;
using UnityEngine;

public class EnemyHitCollision : MonoBehaviour
{
    [SerializeField] private float HitDuration = 0.2f;
    [SerializeField] public Enemy parentEnemy;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerMaster>())
        {
            parentEnemy.HitConnectded(other);
        }
    }


    private void Start()
    {
        if(HitDuration == -1.0f) return;
        Destroy(this , HitDuration);    
    }

    

}
