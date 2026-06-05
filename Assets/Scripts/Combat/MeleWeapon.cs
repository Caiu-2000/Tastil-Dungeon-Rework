
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
    [SerializeField] private TrailRenderer _trail;

    

    public bool DebugBool = false;


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
        if (TryAttack())
        {

        }
    }
    public override void ReleaseAttack()
    {

    }
    public override bool TryAttack()
    {

        if (_attacking || !_equiped) return false;
        BuffManager.Instance.TriggerOnAttack(ParentEntity.gameObject);
        if (ParentEntity._currentStamina >= _stamCost)
        {
            ParentEntity.ReduceStamina(_stamCost);

            _currentCombo += 1;
            if (_currentCombo > _maxCombo) _currentCombo = 1;
            ComboCount = ComboCd;

            _attacking = true;
            StartCoroutine(AttackSecuence());
            return true;
        }
        return false;
    }
    public override Item PicItem(Entity _entity)
    {
        return this;
        // Reescribir segun que item se agarre 
    }

    private IEnumerator AttackSecuence()
    {
 
        // LLAMAR A ANIMACIONES 
        GameManager.Instance.Player.SetAttackAnimation("Attack" , _currentCombo);
        _trail.emitting = true;
        yield return new WaitForSeconds(AttackTimers[_currentCombo -1]);
        DebugBool = true;

   
        float elapsedTime = 0;

        /*Perdoname padre por que eh pecado
         Tengo que hacer una lista que cada frame se recorra y actualice por que es la unica manera que se me ocurre 
        con el codigo en este estado.
        Esto es para que al golpear no se hitee varias veces por golpe a cada enemigo
         
         */
        List<IHittable> AlreadyHitted = new List<IHittable>();
        while (true)
        {
            elapsedTime += Time.deltaTime;

            Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * _reach + ParentEntity.transform.position + new Vector3(0, _vertialOfsset, 0);

            //Collider[] collisions = Physics.OverlapSphere(AttackPos, _collSize, LayerMask.GetMask("Hittable"));
            Collider[] collisions = Physics.OverlapBox(AttackPos, new Vector3(_collSize , _collSize , _collSize) , GameManager.Instance.Player.transform.rotation, LayerMask.GetMask("Hittable"));
            foreach (Collider Hitted in collisions)
            {
                IHittable hitable = Hitted.GetComponent<IHittable>();


                if (hitable == null) continue;
                if (AlreadyHitted.Contains(hitable)) continue;
                if (hitable.GetType() == ParentEntity.GetType()) { continue; }
                
                if (Random.Range(0f, 1f) < _critChance / 100)              //Divido por 100 asi hago que la critchance vaya entre 0 a 1 (pasando por las comas), se prodria haber hecho diferente pero queria usar floats
                {
                    hitable.Hit(_damage * 1.5f, _canKnockback, _knockbackForce, ParentEntity.transform); //CRITICO, aca tendriamos que llamar vfx y toda la cosa
                    BuffManager.Instance.TriggerOnHit(Hitted.gameObject);
                    BuffManager.Instance.TriggerOnCriticalHit(Hitted.gameObject);
                }
                else
                {
                    hitable.Hit(_damage, _canKnockback, _knockbackForce, ParentEntity.transform);
                    BuffManager.Instance.TriggerOnHit(Hitted.gameObject);
                }
                AlreadyHitted.Add(hitable);
            }
            if (elapsedTime > _attackCollDuration) break;
            yield return null;
        }
        DebugBool = false;
        
        yield return new WaitForSeconds(_timeBetwenAttacks);
        _trail.emitting = false;
        _attacking = false;
        
    }
    

    private void OnDrawGizmos()
    {
        return;
        //Esto es solo debug pero tira error de referencia nula en editore por eso esta asi apagado. Es para ver la collision del golpe

        if (!DebugBool) return;
        // Al final tengo que ver como hacer para usar parententity y ver cuando es del player y cuando no
        // O tambien me queda hacer que cada entity tenga un punto de spawn para los ataques
        Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * _reach + ParentEntity.transform.position + new Vector3(0,_vertialOfsset,0);
        //Gizmos.DrawWireSphere(AttackPos , _collSize);
        Gizmos.DrawWireCube(AttackPos, new Vector3(_collSize, _collSize, _collSize));
    }
}
