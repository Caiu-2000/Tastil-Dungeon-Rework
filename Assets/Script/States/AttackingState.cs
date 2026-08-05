using System.Collections;
using UnityEngine;


public class AttackingState : State
{
    [SerializeField] float AttackDuration = 1.0f;
    public bool IsOnCD = false;
    [SerializeField] float CdTime = 1.5f;
    [SerializeField] private float hittWindow = 0.1f;
    [SerializeField] private float parryWindow = 0.1f;
    [SerializeField] private float timeBeforeAttack = 0.25f;

    public override void StartState()
    {
        base.StartState();
        StartCoroutine(SetAttack());
    }


    private IEnumerator SetAttack()
    {
        
        StartCoroutine(countCD());
        _controlledEntity.OnEntityAttacked?.Invoke();
        _controlledEntity._AttackAlreadyConected = false;
        _controlledEntity._animator.SetTrigger("attack");
     
        ParentMachine.AiComponent.ChangeEnabled(false);
        yield return new WaitForSeconds(timeBeforeAttack);
        _controlledEntity.SettParryCollision(parryWindow);
        yield return new WaitForSeconds(parryWindow);
        _controlledEntity.SettAttackCollision(hittWindow);
        yield return new WaitForSeconds(hittWindow);


        yield return new WaitForSeconds(0.25f);



        ParentMachine.ChangeState(DefaultNextState);

    }

    private IEnumerator countCD()
    {
        IsOnCD = true;
        yield return new WaitForSeconds(CdTime);
        IsOnCD = false ;
    }

}
