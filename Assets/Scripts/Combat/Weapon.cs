using UnityEngine;

[RequireComponent(typeof(Animator))]


public class Weapon : Item
{

    [SerializeField] protected float _damage = 10.0f, _knockbackForce = 30.0f;
  
    protected Animator animator;
    [SerializeField]
    protected bool _equiped = false , _readyToAttack = true;
    public float _stamCost = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
       
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
        PlayerInput _input = GameManager.Instance.GetInput();
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
        PlayerInput _input = GameManager.Instance.GetInput();
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
        PlayerInput _input = GameManager.Instance.GetInput();
        if (_input)
        {
            _input.OnAttackPressed -= ChargeAttack;
            _input.OnAttackReleased -= ReleaseAttack;
        }
    }

    public override void Interact(PlayerMaster player = null)
    {
        player.weaponHand.EquipWeapon(this);
    }
    
}
