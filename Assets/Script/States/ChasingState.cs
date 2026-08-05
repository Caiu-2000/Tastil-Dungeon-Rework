using UnityEngine;

public class ChasingState : State
{
    [SerializeField] protected float RangeForAttack = 1.0f;


    [SerializeField] private bool ChaseFire;

    public override void StartState()
    {
        base.StartState();
        ParentMachine.AiComponent.ChangeObjective(GameManager.Instance.Player.transform);
        print("esto se le dio start");
        ParentMachine.AiComponent.ChangeEnabled(true);
    }
    public override void UpdateState()
    {
        if (ParentMachine.AiComponent.GetDistance() < 1) { ParentMachine.ChangeState(DefaultNextState); }
 
    
    }
    public override void PauseState()
    {
        print("Se pauso");
        ParentMachine.AiComponent.ChangeEnabled(false);
    }
}
