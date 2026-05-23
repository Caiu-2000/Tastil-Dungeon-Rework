using UnityEngine;

public class FireBreathEffect : MonoBehaviour, IProjectile
{
    Transform player;
    BuffManager buffManager;
    float timer = 3.0f;
    float timerCd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      transform.rotation = Quaternion.Euler(0, 90, 0);
        player = FindAnyObjectByType<PlayerInput>().gameObject.transform;
        buffManager = FindAnyObjectByType<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        if (timer > timerCd)
            timerCd += Time.deltaTime;
        else
            Destroy(gameObject);
    }
    public void SetTarget(Transform target)
    {
        //player = target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            buffManager.TriggerOnHit(other.gameObject);
        }
    }
    
}
