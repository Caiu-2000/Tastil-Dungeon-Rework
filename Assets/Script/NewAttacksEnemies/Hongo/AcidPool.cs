using UnityEngine;
using System.Collections;

public class AcidPool : MonoBehaviour
{
    [SerializeField] private float tickInterval = 0.5f;
    [SerializeField] private float damage = 1f;
    private bool coroutineRunning = false;
    private void Start()
    {
        StartCoroutine(Destroy());
        transform.position += new Vector3(0, 0.07f, 0); // Para que se vea bien el acido lo muevo un toque para arriba
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !coroutineRunning)
        {
            coroutineRunning = true;
            other.GetComponent<PlayerMaster>().applyDamage(damage);
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(tickInterval);
        coroutineRunning = false;
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
