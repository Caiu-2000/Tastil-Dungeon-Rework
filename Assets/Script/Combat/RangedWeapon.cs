using UnityEngine;

[RequireComponent (typeof(Animator))]
public class RangedWeapon : Weapon
{
    [SerializeField] Proyectile _arrow;
    [SerializeField] Proyectile _SpecialArrow;
    [SerializeField] Transform _firePoint;
    private Animator animator;
    private bool _IsCharging = false;

    private float currentTension = 0f;
    [SerializeField] private float MaxTension = 3.0f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        _firstPosition = transform.position;
        _ItemCollider = GetComponent<Collider>();

    }

    private void Update()
    {
        if (_IsCharging)
        {
            currentTension += Time.deltaTime;
            if (currentTension > MaxTension)
            {
                currentTension = MaxTension;

            }
        }
    }

    public override void ChargeAttack()
    {
        animator.SetTrigger("StartCharge");
        _IsCharging = true;
    }
    public override void ReleaseAttack()
    {
        if (!_IsCharging) return;
        
        _IsCharging=false;
        animator.SetTrigger("Release");

        ShotArrow(_arrow);
    }
    public override void ChargeSpecial()
    {
        base.ChargeSpecial();
        animator.SetTrigger("StartCharge");
        _IsCharging = true;
    }
    public override void ReleaseSpecial()
    {
        base.ReleaseSpecial();
        if (!_IsCharging) return;

        _IsCharging = false;
        animator.SetTrigger("Release");

        ShotArrow(_SpecialArrow);
    }
    private void ShotArrow(Proyectile arrow)
    {
        Proyectile arrowInstance = Instantiate(arrow);
        Vector3 FiringOffset = new Vector3(0, 0.5f, 0);
        arrowInstance.transform.position = GameManager.Instance.Player.transform.position + FiringOffset;
        arrowInstance._speed *= 3 * currentTension;

        Vector3 AimedPos = GameManager.Instance.Player.GetLookDretirection();


        arrowInstance.ChangeDirection(AimedPos);
        arrowInstance._fromPlayer = true;
        arrowInstance._damage = _damage;

        currentTension = 0;
    }

}
