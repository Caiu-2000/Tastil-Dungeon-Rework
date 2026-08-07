
using UnityEngine;




public class Weapon : Item
{

    [SerializeField] protected float _damage = 10.0f, _knockbackForce = 30.0f, _baseDamage = 10.0f;
    [SerializeField] protected float _critChance = 10f; float _baseCritChance = 10f;
    bool guaranteeCrit = false;
    public int WeaponID = 0;

    [SerializeField]
    protected bool   _readyToAttack = true , _chargableAttack = false;
    public float _stamCost = 10.0f;

    public bool _equiped = false;

    protected Entity ParentEntity;
    protected Collider _ItemCollider;

    protected Animator HandAnimator;
    public Sprite crosshair;
    public Sprite hitmarker;
    public Sprite critmarker;

    [SerializeField] protected WeaponHability Hability;


    [SerializeField] protected WeaponAnimations _weaponAnimations;
    protected bool ChargingSpecial = false;
    [SerializeField] protected float SpeciallChargeTime = 0.5f;
    [SerializeField] protected float SpecialStamCost = 10.0f;
    protected float SpecialTimeCount = 0;
    protected bool ReleasedSpecial = false;



    private void Start()
    {
        _ItemCollider = GetComponent<Collider>();
        _firstPosition = transform.position;
    }

    public virtual void ChargeAttack()
    {
        if (_stamCost <= GameManager.Instance.Player._currentStamina)
        {
            GameManager.Instance.Player._currentStamina -= _stamCost;
        }
    }
    public virtual void ReleaseAttack()
    {
      
    }


    
    
    public void ActivateWeapon(Animator handAnimator)
    {

        PlayerInput _input = GameManager.Instance.GetInput();
        HandAnimator = handAnimator;
        Hability.InitialiceHability(GameManager.Instance.Player , this); // Se supone que estaba hecho para que los enemigos tambien manejen las armas. No se llego 
        _equiped = true;
        _readyToAttack = true;
        _ItemCollider.enabled = false;
        if (_input)
        {
            _input.OnAttackPressed += ChargeAttack ;
            _input.OnAttackReleased += ReleaseAttack ;
            _input.OnSpecialReleased += ReleaseSpecial ;
            _input.OnSpecialPressed += ChargeSpecial ;
        }
        SpecialActivation();
    }

    public virtual void SpecialActivation() { }
    public void DeactivateWeapon()
    {
        PlayerInput _input = GameManager.Instance.GetInput();
        _readyToAttack = false;
        _equiped = false;
        _ItemCollider.enabled = true;
        if (_input)
        {
            _input.OnAttackPressed -= ChargeAttack;
            _input.OnAttackReleased -= ReleaseAttack;
            _input.OnSpecialReleased -= ReleaseSpecial;
            _input.OnSpecialPressed -= ChargeSpecial;
        }
        RecursiveChangeLayer(gameObject , 10);
    }

    public virtual bool TryAttack()
    {
        return false;
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
    
    public void SetDamage(float damageMultiplier)
    {
        _damage = _baseDamage * damageMultiplier;
    }
    public void GuaranteeCriticalHit()
    {
        guaranteeCrit = true;
    }
    public void ResetCritChance()
    {
        _critChance = _baseCritChance;
    }
    public void AddCritChance(int critChanceToAdd)
    {
        _critChance += critChanceToAdd;
        if (_critChance > 100)
            _critChance = 100;
    }

    public WeaponAnimations GetAnimations() { return _weaponAnimations; }

    public virtual void ChargeSpecial()
    {

        if (SpecialStamCost <= GameManager.Instance.Player._currentStamina && !ChargingSpecial)
        {
            GameManager.Instance.Player._currentStamina -= SpecialStamCost;
            ChargingSpecial = true;

        }

    }
    public virtual void ReleaseSpecial()
    {
        ReleasedSpecial = true;

    }



    private void Update()
    {
        if (ChargingSpecial)
        {
            SpecialTimeCount += Time.deltaTime;
        }
        if (ReleasedSpecial && SpecialTimeCount > SpeciallChargeTime) { Hability.RunHability(); ReleasedSpecial = false; ChargingSpecial = false; } 
    }

}