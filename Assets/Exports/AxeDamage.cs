using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    [SerializeField] private float Damage = 2.0f;
    private void OnCollisionEnter(Collision collision)
    {
        Entity entidad = collision.gameObject.GetComponent<Entity>();
        entidad.applyDamage(new HittData(Damage));
    }
}
