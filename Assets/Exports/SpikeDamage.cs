using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Entity entidad = other.GetComponent<Entity>();
        entidad.applyDamage(2f);
    }
}
