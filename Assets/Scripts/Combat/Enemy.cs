using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SearchService;


public class Enemy : Entity
{
    private Renderer _renderer;
    protected bool CanAttack = true;
    //[SerializeField] protected AiComponent _ai;
    [SerializeField] protected Animator _animator;



    [SerializeField] protected float _damage;
    [SerializeField] private float _knockBackForce;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
   
        _currentLife = _maxLife;

    }

    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        base.applyDamage(damage, ApplyKnockback, knockbackForce, KnockBackFrom);


        PerkManager.Instance.OnEnemyHitted(this);

        //StartCoroutine(PlayAndFinish("hitted"));
        if (_damCD) return;
        StartCoroutine(CDCounter());
       // _ai.TemporaryDisable(1.0f);
       // _damCD = true;
        

    }

    public override void Die()
    {
        PerkManager.Instance.OnEnemyDeath(this);
        base.Die();
    }


    private IEnumerator CDCounter()
    {
        /*
        _renderer.material.SetColor("_BaseColor", Color.red);
        yield return new WaitForSeconds(_DamageCDTime);
        _damCD = false;
        _renderer.material.SetColor("_BaseColor", Color.white);
        */
        yield return null;
    }


    public virtual void DistanceReached()
    {

    }

    public virtual void SetWalking(bool IsWalking)
    {
        _animator.SetBool("Walking", IsWalking);
    }

    internal void HitConnectded(Collider other)
    {

        other.GetComponent<PlayerMaster>().applyDamage(_damage , true , _knockBackForce , transform);
        PerkManager.Instance.OnPlayerHitted?.Invoke(_damage, this);
    }

    protected IEnumerator PlayAndFinish(string TriggerName)
    {
        _animator.SetTrigger(TriggerName);
        //_ai.ChangeEnabled(false);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        //_ai.ChangeEnabled(true);
    }



}

