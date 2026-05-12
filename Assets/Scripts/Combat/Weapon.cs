using UnityEngine;

[RequireComponent(typeof(Animator))]


public class Weapon : Item
{

    [SerializeField] protected float _damage = 10.0f, _knockbackForce = 30.0f;
    public PlayerInput _input;
    protected Animator animator;
    [SerializeField]
    protected bool _equiped = false , _readyToAttack = true;
    public float _stamCost = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (_equiped) ActivateWeapon();
    }

    public virtual void ChargeAttack()
    {
        print("Se cargo ataque");
    }
    public virtual void ReleaseAttack()
    {
        print(" Se solto ataque");
    }
    
    
    public void ActivateWeapon()
    {
        animator.SetTrigger("Activate");
        _equiped = true;
        _readyToAttack = true;

        if (_input)
        {
            _input.OnAttackPressed += ChargeAttack ;
            _input.OnAttackReleased += ReleaseAttack ;
        }

    }
    public void DeactivateWeapon()
    {
        _readyToAttack = false;
        _equiped = false;
        animator.SetTrigger("Deactivate");
        if (_input)
        {
            _input.OnAttackPressed -= ChargeAttack;
            _input.OnAttackReleased -= ReleaseAttack;
        }
    }

    public virtual float TryAttack()
    {
        return 0.0f;
    }

    public virtual float Attack()
    {
        return 0.0f;
    }


    private void OnDestroy()
    {
        if (_input)
        {
            _input.OnAttackPressed -= ChargeAttack;
            _input.OnAttackReleased -= ReleaseAttack;
        }
    }
}
