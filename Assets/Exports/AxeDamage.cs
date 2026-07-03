using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Entity entidad = collision.gameObject.GetComponent<Entity>();
        entidad.applyDamage(2f);
    }
}
