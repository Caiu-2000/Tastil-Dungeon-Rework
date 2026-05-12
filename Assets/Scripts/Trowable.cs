using UnityEngine;

public class Trowable : Proyectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider collision)
    {
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Entity>().applyDamage(_damage , true , 5 , transform);
            
        }
        if (collision.gameObject.GetComponent<BreacableComponent>())
        {
            collision.gameObject.GetComponent<BreacableComponent>().Breack();
        }
        Destroy(this.gameObject);
        */
    }
}