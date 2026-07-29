
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class ChargeState : State
{
    public bool IsInCooldown = false;
    [SerializeField] float _SpecialCharge , _Specialduration;


    private IEnumerator ChargeSecuence()
    {
        GetComponent<NavMeshAgent>().enabled = false;

        IsInCooldown = true;

        ParentMachine._ai.ChangeEnabled(false);
        float elapsedTime = 0.0f;
    
        ParentMachine._animator.SetTrigger("GoSpecial");


        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _SpecialCharge) { break; }
            ParentMachine._movement.RotateTowards(GameManager.Instance.Player.transform);
            yield return null;
        }
        elapsedTime = 0.0f;

        ParentMachine._animator.SetTrigger("Launch");



        float tempSpeed = ParentMachine._movement.GetSpeed();
        ParentMachine._movement.SetSpeed(20);
        ParentMachine._entity.SettAttackCollision(_Specialduration);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _Specialduration || _AttackAlreadyConected) { break; }
            moveComp.Movement(new Vector2(0, 1));


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
        SpecialInCooldown = false;



    }
}
