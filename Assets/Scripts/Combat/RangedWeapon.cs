using UnityEngine;

[RequireComponent (typeof(Animator))]
public class RangedWeapon : Weapon
{
    [SerializeField] Proyectile _arrow;
    [SerializeField] Transform _firePoint;
    private Animator animator;
    private bool _IsCharging = false;




    private void Start()
    {
        animator = GetComponent<Animator>();
        _firstPosition = transform.position;
        _ItemCollider = GetComponent<Collider>();

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
        Proyectile arrowInstance = Instantiate(_arrow);
        Vector3 FiringOffset = new Vector3(0, 0.5f, 0);
        arrowInstance.transform.position = GameManager.Instance.Player.transform.position + FiringOffset;
        arrowInstance._speed *= 3;

        Vector3 AimedPos = GameManager.Instance.Player.GetLookDretirection();

        
        arrowInstance.ChangeDirection(AimedPos);
        arrowInstance._fromPlayer = true;
        arrowInstance._damage = _damage;
    }

}
