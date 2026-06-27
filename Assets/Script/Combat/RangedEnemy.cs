
using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{
   
    [SerializeField] Transform _collPoint;
    [SerializeField] float _attackTime;
    [SerializeField] Proyectile _proyectile;
    
    public override void DistanceReached()
    {
        if (CanAttack)
        {
            StartCoroutine(PlayAndFinish("attack"));
            StartCoroutine(SettAttackCollision());
            // _animator.SetTrigger("attack");
        }
        

    }

    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {

        base.applyDamage(damage, ApplyKnockback, knockbackForce, KnockBackFrom);

    }



    private IEnumerator SettAttackCollision()
    {
        yield return new WaitForSeconds(_attackTime);
        Proyectile newProyectile = Instantiate(_proyectile);
        newProyectile.transform.position = _collPoint.position;
        newProyectile._damage = _damage;
        //newProyectile.transform.LookAt(_ai.GetObjectiveTransform());
        
    }
}
