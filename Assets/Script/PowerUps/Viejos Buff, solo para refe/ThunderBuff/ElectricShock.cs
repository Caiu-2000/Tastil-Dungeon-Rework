using UnityEngine;

public class ElectricShock : MonoBehaviour, IProjectile
{
    Transform targetPos;
    float speed = 5f;
    BuffManager buffManager;
    //just for thest
    void Start()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
    }    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed*Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPos.position) < 0.05f)
        {
            //just to test things - Fede
            if (Random.Range(0, 100) < 10)
                buffManager.TriggerOnHit(targetPos.gameObject);
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform target)
    {
        targetPos = target;
    }

}
