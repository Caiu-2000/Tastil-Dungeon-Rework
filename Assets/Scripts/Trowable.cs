using UnityEngine;

public class Trowable : Proyectile
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider collision)
    {

        IHittable hitted;
        if( collision.TryGetComponent(out hitted))
        {
            if (hitted.GetType() == typeof(PlayerMaster) && _fromPlayer) { return; }
            hitted.Hit(_damage);
        }
        Destroy(gameObject);    


    }
}