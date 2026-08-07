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
    [SerializeField] private GameObject model;  

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
            GameManager.Instance.Player.ReduceStamina(0);
            currentTension += Time.deltaTime;
            if (currentTension > MaxTension)
            {
                currentTension = MaxTension;

            }
        }
    }

    public override void ChargeAttack()
    {
        if (GameManager.Instance.Player._currentStamina < _stamCost) return;
        animator.SetTrigger("StartCharge");
        _IsCharging = true;
    }
    public override void ReleaseAttack()
    {
        if (!_IsCharging) return;
        GameManager.Instance.Player.ReduceStamina(_stamCost);
        _IsCharging=false;
        animator.SetTrigger("Release");

        ShotArrow(_arrow);
    }
    public override void ChargeSpecial()
    {
        if (GameManager.Instance.Player._currentStamina < SpecialStamCost) return;
        base.ChargeSpecial();
        animator.SetTrigger("StartCharge");
        _IsCharging = true;
    }
    public override void ReleaseSpecial()
    {
        base.ReleaseSpecial();
        GameManager.Instance.Player.ReduceStamina(SpecialStamCost);
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
        arrowInstance._damage = _damage * (currentTension +1);

        currentTension = 0;
    }
    public override void SpecialActivation()
    {
        model.transform.localPosition = new Vector3(0,0,0);//new Vector3(-0.00139999995f, -0.000199999995f, -9.99999975e-05f);
        model.transform.localRotation = Quaternion.Euler(new Vector3(278.091522f, 276.280792f, 333.610443f));
        model.transform.localScale = new Vector3(0.000160410389f, 0.000160410389f, 0.000160410433f);
    }
}
