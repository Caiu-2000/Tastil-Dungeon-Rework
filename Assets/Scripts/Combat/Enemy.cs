
using System.Collections;
using UnityEngine;



public class Enemy : Entity
{
    [SerializeField] private Renderer _renderer;
    protected bool CanAttack = true;
    [SerializeField] protected AiComponent _ai;
    [SerializeField] public Animator _animator;

    [SerializeField] protected float _damage;
    [SerializeField] private float _knockBackForce;

    public bool CanAnimHitted = true;

    private void Awake()
    {

        _currentLife = _maxLife;

    }

    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        if (_damCD) { return; }
        base.applyDamage(damage, ApplyKnockback, knockbackForce, KnockBackFrom);


        PerkManager.Instance.OnEnemyHitted(this);
        print("Hasta aca se llego bien");
        if (CanAnimHitted) _animator.SetTrigger("hitted");
        
        StartCoroutine(CDCounter());

    }

    public override void Die()
    {
        PerkManager.Instance.OnEnemyDeath(this);
        BuffManager.Instance.TriggerOnEnemyDeath(this.gameObject);
        _animator.SetTrigger("Death");

    }


    protected IEnumerator CDCounter()
    {
        _renderer.material.SetColor("_BaseColor", Color.red);
        _damCD = true;
        yield return new WaitForSeconds(_DamageCDTime);
        _damCD = false;
        _renderer.material.SetColor("_BaseColor", Color.white);

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
        PerkManager.Instance.OnPlayerHitted?.Invoke(_damage, this);
    }

    public IEnumerator PlayAndFinish(string TriggerName)
    {
        _animator.SetTrigger(TriggerName);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

    }

    public virtual void ApplyParry()
    {

    }

}

