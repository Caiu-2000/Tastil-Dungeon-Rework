
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleWeapon : Weapon
{
    
    
    [SerializeField] private bool _canKnockback = true;
    [SerializeField] private int _maxCombo = 3, _currentCombo = 0;
    [SerializeField] private List<float> AttackTimers = new List<float>();
    private float ComboCd = 3.0f, ComboCount = 0.0f;


    [SerializeField] private float  _reach = 1.0f , _collSize = 0.5f , _vertialOfsset = 0.5f , _attackCollDuration = 0.1f , _timeBetwenAttacks = 1.0f;


    

    public bool _attacking = false,  _activeHitt = false;

    private void Update()
    {
        if (ComboCount  > 0)
        {
            ComboCount -= Time.deltaTime;
        }
        else
        {
            _currentCombo = 0;
        }
        
        if (!_equiped) return;
    }
    public override void ChargeAttack()
    {
        print("Se cargo ataque");
    }
    public override void ReleaseAttack()
    {
        TryAttack();
    }
    public override void TryAttack()
    {
        print("Se intento atacar");
        if (_attacking || !_equiped) return;
        if (ParentEntity._currentStamina >= _stamCost)
        {
            ParentEntity.ReduceStamina(_stamCost);

            _currentCombo += 1;
            if (_currentCombo > _maxCombo) _currentCombo = 1;
            ComboCount = ComboCd;

            _attacking = true;
            StartCoroutine(AttackSecuence());
        }
    }




    public override Item PicItem(Entity _entity)
    {
        return this;
        // Reescribir segun que item se agarre 
    }

    private IEnumerator AttackSecuence()
    {
        print("Empezo la secuencia de ataque");
        // LLAMAR A ANIMACIONES 

        yield return new WaitForSeconds(AttackTimers[_currentCombo -1]);
        Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * _reach + ParentEntity.transform.position + new Vector3(0, _vertialOfsset, 0);

        Collider[] collisions = Physics.OverlapSphere(AttackPos , _collSize , LayerMask.GetMask("Hittable" ));

        foreach (Collider Hitted in collisions)
        {
            print("Una Collision");
            IHittable hitable = Hitted.GetComponent<IHittable>();

            if (hitable == null) continue;
            

            hitable.Hit(_damage, _canKnockback, _knockbackForce, ParentEntity.transform);

        }
        yield return new WaitForSeconds(_timeBetwenAttacks);
        _attacking = false;
    }








    private void OnDrawGizmos()
    {
        // Al final tengo que ver como hacer para usar parententity y ver cuando es del player y cuando no
        // O tambien me queda hacer que cada entity tenga un punto de spawn para los ataques
        Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * _reach + ParentEntity.transform.position + new Vector3(0,_vertialOfsset,0);
        Gizmos.DrawWireSphere(AttackPos , _collSize);
    }
}
