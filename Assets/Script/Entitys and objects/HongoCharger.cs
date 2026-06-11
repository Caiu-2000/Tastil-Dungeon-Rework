
using System.Collections;
using UnityEngine;

public class HongoCharger : Enemy
{
    [SerializeField] MovementComponent _moveComp;
    [SerializeField] float _SpecialCD = 2.0f;
    [SerializeField] float _specialDuration = 1.0f;
    [SerializeField] float _specialPreparationTime = 1.0f;
    [SerializeField] float _speedMult = 2.0f;
    [SerializeField] private EnemyHitCollision CollisionForbody;
    
    private bool _attackInProgress = false;
    private Vector2 _attackDir;
    
    public bool IsOnBurrow = false;

    public override void DistanceReached()
    {
        /*
        if (CanAttack)
        {
            StartCoroutine(SpecialAttack());
            
        }
        */

    }
    //private void Update()
    //{
    //    //if (_attackInProgress)
    //    //{
    //    //    _moveComp.Movement(_attackDir);
             
    //    //}
    //}

    // Este es el ataque de carga que quedo con error de referencia nula el movmeent componenet
    // Ahora mismo lo dejo para arreglar
    private IEnumerator SpecialAttack()
    {
        yield return null;
        
    //    float BaseSpeed = _moveComp.GetSpeed();
    //    _ai.ChangeEnabled(false);
        
    //    CanAttack = false;
    //    _animator.SetTrigger("StartSpecial");
    //    print("Empezo el ataque especial");
    //    yield return new WaitForSeconds(_specialPreparationTime);
   
    //    _moveComp.ChangeDamping(1);
    //    print("Comenzo el ataque con velocidad extra");
    //    _moveComp.SetSpeed(BaseSpeed *  _speedMult);
    //    _attackInProgress = true;
    //    Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;
    //    //_attackDir = new Vector2(dir.x, dir.z);
        
    //    _moveComp.ApplyKnockback(dir , 50.0f); 

    //    yield return new WaitForSeconds(_specialDuration);
    //    _attackInProgress = false;
    //    print("Se termino el ataque");
    //    _animator.SetTrigger("SpecialFinished");
    //    _moveComp.SetSpeed(BaseSpeed);
    //    _ai.ChangeEnabled(true);
    //    _moveComp.ChangeDamping(100);
    //    StartCoroutine(CountCDforSpecial());
    }

    private IEnumerator CountCDforSpecial()
    {
        yield return new WaitForSeconds(_specialDuration);
        CanAttack = true;
    }

    public override void Die()
    {
        _roomController?.OnEnemyDied(this);
        _animator.SetTrigger("Died");
        CollisionForbody.enabled = false;
        CanAnimHitted = false;
        float time = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Destroy(gameObject, time);
        
            
    }


}
