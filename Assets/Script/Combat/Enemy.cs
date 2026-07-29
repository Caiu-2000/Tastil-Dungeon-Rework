
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Enemy : Entity
{
    [SerializeField] private Renderer _renderer;
    protected bool CanAttack = true;
    [SerializeField] protected AiComponent _ai;
    [SerializeField] public Animator _animator;
    [SerializeField] EnemyHitCollision _coll;
    [SerializeField] protected float _damage;
    [SerializeField] private float _knockBackForce;
    [SerializeField] ParryCollision _parryCollision;
    [SerializeField] Transform _collPoint;
    protected RoomController _roomController;
    public void SetRoomController(RoomController rc) => _roomController = rc;

    public bool CanAnimHitted = true;
    public bool KeepDead = false;

    [SerializeField] protected EntitySoundComponent SoundEmitter = new EntitySoundComponent();

    [SerializeField] private StateMachine StateMachine;

    protected MovementComponent moveComp; 
    private void Awake()
    {

        _currentLife = _maxLife;

    }

    private void Start()
    {
        
        moveComp = GetComponent<MovementComponent>();
        
        SoundEmitter.InitializeThis(this);
    }

    public override void applyDamage(HittData hitt)
    {
        if (_damCD) { return; }
        base.applyDamage(hitt);

        if (CanAnimHitted && _animator) _animator.SetTrigger("hitted");
        
        StartCoroutine(CDCounter());

    }

    public override void Die()
    {
        OnEntityDead?.Invoke();
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

        other.GetComponent<PlayerMaster>().applyDamage(new HittData(_damage , this , transform.position));
        //PerkManager.Instance.OnPlayerHitted?.Invoke(_damage, this);
    }
    internal virtual void HitConnectded(PlayerMaster player)
    {
        player.applyDamage(new HittData(_damage, this, transform.position));
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

    public void SettAttackCollision(float time = -2.0f)
    {
        EnemyHitCollision newColl = Instantiate(_coll);
        newColl.ChangeDuration(time);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.transform.SetParent(_collPoint.transform, true);
        newColl.parentEnemy = this;
    }
    public void SettParryCollision()
    {
        ParryCollision newColl = Instantiate(_parryCollision);
        newColl.ChangeDuration(-2.0f);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.ParentEnemy = this;
    }


}

