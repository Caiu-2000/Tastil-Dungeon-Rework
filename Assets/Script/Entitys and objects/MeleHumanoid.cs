
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleHumanoid : Enemy
{
    [SerializeField] EnemyHitCollision _coll;
    [SerializeField] ParryCollision _parryCollision;
    [SerializeField] Transform _collPoint;
    // Si robe las variables de las armas para hacerlo al enemigo. 
    // Que no se note que podria integrar que agarren armas

    [SerializeField] private int _maxCombo = 3, _currentCombo = 0;

    [SerializeField] private List<AttackData> Attacks;

    private bool _AttackAlreadyConected = false;
    private bool _ParryAlreadyConected = false;
    [SerializeField] EnemyAttackSincronizer1 _AttackSync;











    private void Awake()
    {

        _currentLife = _maxLife;
        _maxCombo = Attacks.Count ;
    }


    public override void DistanceReached()
    {
        if (CanAttack)
        {
            CanAttack = false;
            _currentCombo += 1;
            if (_currentCombo > _maxCombo) 
            { 
                _currentCombo = 1;
            }

            StartCoroutine(SetAttack());

        }
        
        
    }


    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {

        base.applyDamage(damage, ApplyKnockback, knockbackForce, KnockBackFrom);
    }


    private IEnumerator SetAttack()
    {
        _AttackAlreadyConected = false;
        _animator.SetTrigger("attack");
        _animator.SetInteger("AttackID", _currentCombo - 1);
        _ai.ChangeEnabled(false);
        yield return new WaitForSeconds(Attacks[_currentCombo - 1 ].AttackCD);
        _ai.ChangeEnabled(true);
        CanAttack = true;
    }

    internal override void HitConnectded(Collider other)
    {
        other.GetComponent<PlayerMaster>().applyDamage(_damage, true, Attacks[_currentCombo -1].KnockbackForce, transform);
        _AttackAlreadyConected = true;
        PerkManager.Instance.OnPlayerHitted?.Invoke(_damage, this);
    }

    private IEnumerator WaitToCanAttack(float time)
    {
        yield return new WaitForSeconds(time);
        CanAttack = true;
        _ai.ChangeEnabled(true);
        _animator.SetTrigger("ParriedFinished");
    }


    public void Stun(bool Parried , float TimeStunned)
    {
        _ai.ChangeEnabled(false);
        StopAllCoroutines();
        StartCoroutine(WaitToCanAttack(TimeStunned));
        
        if (Parried)
        {
            _animator.SetTrigger("Parried");
        }
    }

    public override void ApplyParry()
    {
        Stun(true, 2.0f);


    }

    private void Update()
    {
        if (_AttackSync && !_AttackAlreadyConected)
        {
            if (_AttackSync.ParryWindowReady)
            {
                SettParryCollision();
            }
            if (_AttackSync.AttackWindowReady)
            {
                SettAttackCollision();
            }
        }
    }

    private void SettAttackCollision()
    {
        EnemyHitCollision newColl = Instantiate(_coll);
        newColl.ChangeDuration(-2.0f);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.parentEnemy = this;
    }
    private void SettParryCollision()
    {
        ParryCollision newColl = Instantiate(_parryCollision);
        newColl.ChangeDuration(-2.0f);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.ParentEnemy = this;
    }

}
