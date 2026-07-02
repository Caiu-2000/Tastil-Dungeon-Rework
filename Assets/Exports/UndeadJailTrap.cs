using UnityEngine;

public class UndeadJailTrap : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float dropTimer = 5f;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private Rigidbody jailRigidbody;
    [SerializeField] private LayerMask groundLayer;
    private bool hasDropped = false;
    private bool hasSpawned = false;
    private float elapsed = 0f;

    private void Update()
    {
        if (hasDropped) return;
        elapsed += Time.deltaTime;
        if (elapsed >= dropTimer) Drop();
    }

    public void TakeDamage(int amount)
    {
        if (hasDropped) return;
        health -= amount;
        if (health <= 0) Drop();
    }

    private void Drop()
    {
        hasDropped = true;
        jailRigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasSpawned || !hasDropped) return;
        if (((1 << collision.gameObject.layer) & groundLayer) == 0) return;
        hasSpawned = true;
        Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
