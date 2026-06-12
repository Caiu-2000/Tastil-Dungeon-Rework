using System;
using System.Collections;
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
        }
    }


    private void Start()
    {
        if(HitDuration == -1.0f) return;
        
            Destroy(this.gameObject, HitDuration);    
    }

    public void ChangeDuration(float duration)
    {
        if (duration == -2.0f)
        {
            StartCoroutine(DestroyNextFrame());
            return;
        }
        Destroy(    this.gameObject, duration);
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return null;
        Destroy(this.gameObject);
    }

    

}
