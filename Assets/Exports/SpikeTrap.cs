using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] float activationDelay = 1.0f;
    [SerializeField] float activeDuration = 1.5f;
    [SerializeField] float cooldown = 2f;
    [SerializeField] GameObject spikes;
    [SerializeField] Collider damageCollider;
    [SerializeField] float damage = 2f;
    bool isTriggered = false;
    [SerializeField] Transform upPos;
    [SerializeField] Transform downPos;
    [SerializeField] float speed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if(isTriggered) return;
        StartCoroutine(SpikeRoutine());
    }
    private void OnTriggerStay(Collider other)
    {
        if (isTriggered) return;
        StartCoroutine(SpikeRoutine());
    }
    private IEnumerator SpikeRoutine()
    {
        isTriggered = true;
        yield return new WaitForSeconds(activationDelay);
        while (Vector3.Distance(spikes.transform.position, upPos.position) > 0.05f)
        {
            spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, upPos.position, speed*Time.deltaTime);
            yield return null;
        }
        spikes.transform.position = upPos.position;
        damageCollider.enabled = true;
        yield return new WaitForSeconds(activeDuration);
        damageCollider.enabled = false;
        while(Vector3.Distance(spikes.transform.position, downPos.position) > 0.05f)
        {
            spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, downPos.position, speed * Time.deltaTime);
            yield return null;
        }
        spikes.transform.position = downPos.position;
        yield return new WaitForSeconds(cooldown);
        isTriggered = false;
    }
}
