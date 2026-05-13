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
        animator = GetComponent<Animator> ();
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
        arrowInstance.transform.position = _firePoint.position;


        Vector3 AimedPos = GameManager.Instance.Player.GetLookDretirection();

        
        arrowInstance.transform.LookAt( AimedPos+ _firePoint.position);
        arrowInstance._fromPlayer = true;
        arrowInstance._damage = _damage;
    }

}
