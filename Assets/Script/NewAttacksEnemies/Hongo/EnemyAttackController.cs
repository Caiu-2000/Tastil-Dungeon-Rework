using UnityEngine;
using System.Collections;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private float spitCooldown = 5f;
    [SerializeField] private float burrowCooldown = 8f;
    [SerializeField] private float rollCooldown = 1.5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 7f;
    [SerializeField] private float telegraphDuration = 2f;
    [SerializeField] private GameObject spitPrefab;
    [SerializeField] private GameObject mouth;

    private bool spitReady = true;
    private bool burrowReady = true;
    private bool isRolling = false;
    private bool isAttacking = false;
    private Renderer _renderer;
    private PlayerMaster player;

    private void Start()
    {
        player = GameManager.Instance.Player;
        _renderer = GetComponent<Renderer>();
        StartCoroutine(RollCooldown());
    }

    private void Update()
    {
        if (!isRolling && !isAttacking && CheckRange())
            DecideAttack();
    }

    private bool CheckRange()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        return dist > minDistance && dist < maxDistance;
    }

    private void DecideAttack()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        float spitWeight = Mathf.InverseLerp(minDistance, maxDistance, dist);
        float roll = Random.Range(0f, 1f);

        if (roll < spitWeight)
        {
            if (spitReady) Spit();
            else StartCoroutine(RollCooldown());
        }
        else
        {
            if (burrowReady) Burrow();
            else StartCoroutine(RollCooldown());
        }
    }

    private void Spit()
    {
        isAttacking = true;
        spitReady = false;
        var spitGO = Instantiate(spitPrefab, mouth.transform.position, Quaternion.identity);
        spitGO.GetComponent<SpitProjectile>().SetTarget(player.transform.position);
        isAttacking = false;
        StartCoroutine(SpitCooldown());
        StartCoroutine(RollCooldown());
    }

    private void Burrow()
    {
        isAttacking = true;
        burrowReady = false;
        StartCoroutine(BurrowSequence());
    }

    private IEnumerator BurrowSequence()
    {
        // Cambiar los triggers por el nombre real
        GetComponent<Animator>().SetTrigger("Burrow");
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);

        _renderer.enabled = false;
        Vector3 targetPos = player.transform.position;
        transform.position = new Vector3(targetPos.x, transform.position.y - 5f, targetPos.z);

        yield return new WaitForSeconds(telegraphDuration);

        transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
        _renderer.enabled = true;
        GetComponent<Animator>().SetTrigger("PopUp");
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);

        isAttacking = false;
        StartCoroutine(BurrowCooldown());
        StartCoroutine(RollCooldown());
    }

    private IEnumerator SpitCooldown()
    {
        yield return new WaitForSeconds(spitCooldown);
        spitReady = true;
    }

    private IEnumerator BurrowCooldown()
    {
        yield return new WaitForSeconds(burrowCooldown);
        burrowReady = true;
    }

    private IEnumerator RollCooldown()
    {
        isRolling = true;
        yield return new WaitForSeconds(rollCooldown);
        isRolling = false;
    }
}
