using UnityEngine;

public class ChasingState : State
{
    [SerializeField] protected float RangeForAttack = 1.0f;


    [SerializeField] private bool ChaseFire;

    public override void StartState()
    {
        base.StartState();
        ParentMachine.AiComponent.ChangeObjective(GameManager.Instance.Player.transform);
    
    }
    public override void UpdateState()
    {

    /*
        ParentMachine._movement.Move(ParentMachine._ai.DirectionTowards(GeneralHandler.player.transform.position));

        if (Vector3.Distance(this.transform.position, GeneralHandler.player.transform.position) < RangeForAttack)
        {
            if (!ChargeState.ChargeInCD)
            {
                ParentMachine.ChangeState(ChargeState);
            }
            else if (!Attack.ChargeInCD)
            {
                ParentMachine.ChangeState(Attack);
            }
        }
    */
    
    }
}
