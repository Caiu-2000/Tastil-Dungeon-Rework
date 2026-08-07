using UnityEngine;

public class GlobalExplotion : MonoBehaviour
{
    public bool DontDamagePlayer = false;
    public float Damage = 10.0f;
    void Start()
    {
        SoundManager.instance.Play(SoundTypes.explosion);
        Collider[] colliders    = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        
        foreach(Collider collider in colliders)
        {
       
            if (collider.TryGetComponent<IHittable>(out IHittable hitted))
            {
                if (collider.GetComponent<PlayerMaster>() && DontDamagePlayer) continue;
                hitted.Hit(Damage);
       
            }
        }


        Destroy(gameObject , 0.5f);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is IHittable)
        {
            if (other.GetComponent<PlayerMaster>() && DontDamagePlayer) return;
            other.GetComponent<IHittable>().Hit(Damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position , GetComponent<SphereCollider>().radius);
    }

}
