using UnityEngine;

public class orbMove : MonoBehaviour, IProjectile
{
    PlayerMaster player;
    BuffManager manager;
    Transform enemy;
    public void SetTarget(Transform target)
    {
        enemy = target;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMaster>();
        manager = FindAnyObjectByType<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
        if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
        {
            player.Heal(player.GetMaxLife() * 0.33f);
            manager.SpawnScreenVfx("Heal", 1f);
            Destroy(gameObject);
        }
    }
}
