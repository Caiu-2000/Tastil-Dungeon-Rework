using System.Collections;
using UnityEngine;

public class MeleHumanoid : Enemy
{
    [SerializeField] EnemyHitCollision _coll;
    [SerializeField] Transform _collPoint;
    [SerializeField] float _attackTime;
    [SerializeField] float _attackCooldown = 1.5f;
    public override void DistanceReached()
    {
        if (CanAttack)
        {
            StartCoroutine(PlayAndFinish("attack"));
            StartCoroutine(SettAttackCollision());
           // _animator.SetTrigger("attack");
        }

        
    }

   

    private IEnumerator SettAttackCollision()
    {
        CanAttack = false;
        yield return new WaitForSeconds(_attackTime);
        EnemyHitCollision newColl = Instantiate(_coll);
        newColl.ChangeDuration(0.1f);
        newColl.transform.position = _collPoint.position;
        newColl.transform.rotation = _collPoint.transform.rotation;
        newColl.parentEnemy = this;
        yield return new WaitForSeconds(_attackCooldown);
        CanAttack = true;
    }




}
