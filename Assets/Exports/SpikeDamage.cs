using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private float damage = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        Entity entidad = other.GetComponent<Entity>();
        entidad.applyDamage(new HittData(damage));
    }
}
