
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Enemy : Entity
{
    [SerializeField] private Renderer _renderer;
    protected bool CanAttack = true;
    [SerializeField] protected AiComponent _ai;
    [SerializeField] public Animator _animator;

    [SerializeField] protected float _damage;
    [SerializeField] private float _knockBackForce;
    
    protected RoomController _roomController;
    public void SetRoomController(RoomController rc) => _roomController = rc;

    public bool CanAnimHitted = true;
    protected bool KeepDead = false;

    [SerializeField] protected SoundEmitterComponent SoundEmitter = new SoundEmitterComponent();


    protected MovementComponent moveComp; 
    private void Awake()
    {

        _currentLife = _maxLife;

    }

    private void Start()
    {
        
        moveComp = GetComponent<MovementComponent>();
        SoundEmitter.InitializeThis();
    }

    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        if (_damCD) { return; }
        base.applyDamage(damage, ApplyKnockback, knockbackForce, KnockBackFrom);

        if (CanAnimHitted && _animator) _animator.SetTrigger("hitted");
        
        StartCoroutine(CDCounter());

    }

    public override void Die()
    {
       
        BuffManager.Instance?.TriggerOnEnemyDeath(this.gameObject);
        if(_animator) _animator.SetTrigger("Death");
        _roomController?.OnEnemyDied(this);
        Destroy(gameObject);
        
    }


    protected IEnumerator CDCounter()
    {
        if (_renderer) _renderer.material.SetColor("_BaseColor", Color.red);
        _damCD = true;
        yield return new WaitForSeconds(_DamageCDTime);
        _damCD = false;
        if (_renderer) _renderer.material.SetColor("_BaseColor", Color.white);

    }


    public virtual void DistanceReached()
    {

    }

    public virtual void SetWalking(bool IsWalking)
    {
        _animator.SetBool("Walking", IsWalking);
    }

    internal virtual void HitConnectded(Collider other)
    {

        other.GetComponent<PlayerMaster>().applyDamage(_damage , true , _knockBackForce , transform);
        //PerkManager.Instance.OnPlayerHitted?.Invoke(_damage, this);
    }

    public IEnumerator PlayAndFinish(string TriggerName)
    {
        _animator.SetTrigger(TriggerName);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

    }

    public virtual void ApplyParry()
    {

    }

    private void Update() { 
    
     if (KeepDead)
        {
            _ai.ChangeEnabled(false);

        }
    }


    public virtual void SpecialDistanceReached()
    {

    }


    protected void RotateTowards(Transform Objective)
    {
        Vector3 objPosition = Objective.position;

        objPosition.y = this.gameObject.transform.position.y;

        Vector3 Direction = objPosition - transform.position;

        transform.rotation = Quaternion.LookRotation(Direction);
    }

}

