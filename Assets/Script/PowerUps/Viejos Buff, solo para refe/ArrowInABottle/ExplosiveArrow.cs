
using UnityEngine;

public class ExplosiveArrow : Proyectile
{
    [SerializeField] private GlobalExplotion explotion;

    protected override void CallDestroy()
    {
        Instantiate(explotion , transform.position , Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
