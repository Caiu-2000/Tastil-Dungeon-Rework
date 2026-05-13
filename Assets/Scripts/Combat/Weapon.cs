using UnityEngine;



public class Weapon : Item
{

    [SerializeField] protected float _damage = 10.0f, _knockbackForce = 30.0f;
  

    [SerializeField]
    protected bool _equiped = false , _readyToAttack = true;
    public float _stamCost = 0.0f;

    protected Entity ParentEntity;

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
        print(GameManager.Instance.GetInput());
        PlayerInput _input = GameManager.Instance.GetInput();

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

        if (_input)
        {
            _input.OnAttackPressed -= ChargeAttack;
            _input.OnAttackReleased -= ReleaseAttack;
        }
    }

    public virtual void TryAttack()
    {
        return;
    }

    public virtual void Attack()
    {
        return ;
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
        player._inventory.AddItem(this);
    }

    public void SetParentEntity(Entity NewParent)
    {
        ParentEntity = NewParent;
    }
    
}
