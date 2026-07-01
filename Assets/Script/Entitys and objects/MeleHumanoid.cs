
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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




    [SerializeField] private float _SpecialCharge = 1;
    [SerializeField] private float _Specialduration = 1.5f;



    private bool SpecialInProgress = false;
    private bool SpecialInCooldown = false;



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
            CanAnimHitted = false;
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
        CanAnimHitted = true;
        CanAttack = true;
    }

    internal override void HitConnectded(Collider other)
    {
     
        other.GetComponent<PlayerMaster>().applyDamage(_damage, true, Attacks[_currentCombo -1].KnockbackForce, transform);
        _AttackAlreadyConected = true;
      
    }

    private IEnumerator WaitToCanAttack(float time)
    {
        yield return new WaitForSeconds(time);
        CanAttack = true;
        _ai.ChangeEnabled(true);
        SpecialInProgress = false;
        _animator.SetTrigger("ParriedFinished");
        StartCoroutine(RecoverspecialTime());
        CanAnimHitted = true;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private IEnumerator RecoverspecialTime()
    {
        yield return new WaitForSeconds(1.0f);
        SpecialInCooldown = false;
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
        SoundEmitter.PlaySound(SoundTypes.Hit);
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
        if (KeepDead)
        {
            _ai.ChangeEnabled(false);

        }

    }

    private void SettAttackCollision(float time = -2.0f)
    {
        EnemyHitCollision newColl = Instantiate(_coll);
        newColl.ChangeDuration(time);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.transform.SetParent(_collPoint.transform, true);
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

    public override void Die()
    {
     
        _animator.SetTrigger("Death");
        _ai.ChangeEnabled(false);
        Destroy(gameObject, 3.0f);
        _roomController?.OnEnemyDied(this);
        BuffManager.Instance?.TriggerOnEnemyDeath(this.gameObject);
    }



    public override void SpecialDistanceReached()
    {
       
        if (!SpecialInCooldown && !SpecialInProgress) { StartCoroutine(ChargeSecuence()); }
    }


    private IEnumerator ChargeSecuence()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        SpecialInProgress = true;
        CanAttack = false;
        CanAnimHitted = false;
        SpecialInCooldown = true;
        _ai.ChangeEnabled(false);
        float elapsedTime = 0.0f;
        _currentCombo = 1;
        _animator.SetTrigger("GoSpecial");


        while (true) 
        { 
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _SpecialCharge) { break; }
            RotateTowards(GameManager.Instance.Player.transform);
            yield return null;
        }
        elapsedTime = 0.0f;
    
        _animator.SetTrigger("Launch");


        
        float tempSpeed = moveComp.GetSpeed();
        moveComp.SetSpeed(20);
        SettAttackCollision(_Specialduration);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _Specialduration || _AttackAlreadyConected) { break; }
            moveComp.Movement(new Vector2(0,1));
            
            
            yield return null;
        }
        moveComp.SetSpeed(tempSpeed);
        _AttackAlreadyConected = false;
        _animator.SetTrigger("FInish");
        _AttackAlreadyConected = false;
        SpecialInProgress = false;
        GetComponent<NavMeshAgent>().enabled = true;
        CanAttack = true;
        CanAnimHitted = true;
        _ai.ChangeEnabled(true);
        yield return new WaitForSeconds(5);
        SpecialInCooldown =false;



    }




}
