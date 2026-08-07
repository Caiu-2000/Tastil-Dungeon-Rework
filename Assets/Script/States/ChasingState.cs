using UnityEngine;

public class ChasingState : State
{
    [SerializeField] protected float RangeForAttack = 1.0f;


    [SerializeField] private bool ChaseFire;

    public override void StartState()
    {
          
        ParentMachine.AiComponent.ChangeObjective(GameManager.Instance.Player.transform);
        
        ParentMachine.AiComponent.ChangeEnabled(true);
        _controlledEntity._animator.SetBool("Walking", true);
    }
    public override void UpdateState()
    {
        if (ParentMachine.AiComponent.GetDistance() < 1 && !DefaultNextState.IsOnCd) {
        
            ParentMachine.ChangeState(DefaultNextState);
            _controlledEntity._animator.SetBool("Walking", false);
        }
 
    
    }
    public override void PauseState()
    {
   
        ParentMachine.AiComponent.ChangeEnabled(false);
    }
}
