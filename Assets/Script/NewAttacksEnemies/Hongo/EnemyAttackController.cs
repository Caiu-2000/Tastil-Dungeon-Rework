using UnityEngine;
using System.Collections;
[DefaultExecutionOrder(2)]

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private float spitCooldown = 5f;
    [SerializeField] private float burrowCooldown = 8f;
    [SerializeField] private float rollCooldown = 1.5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 17f;
    [SerializeField] private float telegraphDuration = 2f;
    [SerializeField] private GameObject spitPrefab;
    [SerializeField] private GameObject mouth;
    [SerializeField]CapsuleCollider hitCollider;
    [SerializeField] CapsuleCollider BodyCollider;
    [SerializeField] GameObject TelegraphSprite;
    [SerializeField] private AiComponent _ai;
    private bool spitReady = true;
    private bool burrowReady = true;
    private bool isRolling = false;
    private bool isAttacking = false;
    //private Renderer _renderer;
    [SerializeField]GameObject modelo;
    [SerializeField]private PlayerMaster player;

    [SerializeField] private HongoCharger HongoMaster;

    [SerializeField] private float _SpitTime;

    private void Start()
    {
        player = GameManager.Instance.Player;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "HitCollision")
                hitCollider = child.GetComponent<CapsuleCollider>();
        }
        BodyCollider = BodyCollider.GetComponent<CapsuleCollider>();
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
        StartCoroutine(SpitSequence());
    }

    private void Burrow()
    {
        isAttacking = true;
        burrowReady = false;
        StartCoroutine(BurrowSequence());
    }

    private IEnumerator BurrowSequence()
    {
       
        
        HongoMaster.IsOnBurrow = true;
        HongoMaster.CanAnimHitted = false;
        Animator animator = GetComponent<Animator>();
        HongoMaster._animator.SetTrigger("Burrow");
     
        // Cambiar los triggers por el nombre real
        yield return new WaitForSeconds(1.5f/*GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length*/);
        TelegraphSprite.GetComponent<SpriteRenderer>().enabled = true;
        hitCollider.enabled = false;
        BodyCollider.enabled = false;
        modelo.SetActive(false);
        Vector3 targetPos = player.transform.position;
        transform.position = new Vector3(targetPos.x, transform.position.y - 5f, targetPos.z);
        hitCollider.enabled = false;
        yield return new WaitForSeconds(telegraphDuration);

        transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
        BodyCollider.enabled = true;
        hitCollider.enabled = true;
        HongoMaster._animator.SetTrigger("ExitBurrow");
        modelo.SetActive(true);
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        HongoMaster.IsOnBurrow = false;
        isAttacking = false;
        HongoMaster.CanAnimHitted = true;
        TelegraphSprite.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(BurrowCooldown());
        StartCoroutine(RollCooldown());
    }

    private IEnumerator SpitCooldown()
    {
        yield return new WaitForSeconds(spitCooldown);
        spitReady = true;
    }

    private IEnumerator SpitSequence()
    {
        isAttacking = true;
        spitReady = false;

        HongoMaster._animator.SetTrigger("Spit");
        yield return new WaitForSeconds(_SpitTime);
        var spitGO = Instantiate(spitPrefab, mouth.transform.position, Quaternion.identity);
        spitGO.GetComponent<SpitProjectile>().SetTarget(player.transform.position);
        spitGO.GetComponent<SpitProjectile>().SetHongo(HongoMaster);
        isAttacking = false;
        StartCoroutine(SpitCooldown());
        StartCoroutine(RollCooldown());
        yield return null;
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
