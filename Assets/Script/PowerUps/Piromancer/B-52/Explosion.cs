using UnityEngine;

public class Explosion : MonoBehaviour, IProjectile
{
    Transform enemy;
    BuffManager manager;
    public void SetTarget(Transform target)
    {
        enemy = target;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<BuffManager>();
        Collider[] collisions = Physics.OverlapSphere(enemy.transform.position, 3f);
        foreach (Collider collider in collisions)
        {
            if (collider.gameObject.CompareTag("Enemy") && collider.gameObject.transform != enemy)
                manager.SpawnProjectile("Fire", collider.gameObject.transform.position, collider.gameObject.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
