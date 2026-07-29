
using UnityEngine;


public  class State : MonoBehaviour
{

    [SerializeField] protected string AnimationTrigger;
    [SerializeField] protected State DefaultNextState;
    [SerializeField] public bool IsPausable = true;
    protected StateMachine ParentMachine;
    protected Enemy enemy;

    public void InitialiceState(StateMachine Machine , Enemy entity)
    {
        ParentMachine = Machine;
        enemy = entity;
    }

    public virtual void StartState()
    {
       
    }

    public virtual void StopState() 
    {
    
    }

    public virtual void UpdateState()
    {
       
    }

    public virtual void PauseState()
    {

    }
    public virtual void ResumeState()
    {

    }

    public virtual void PhysicsUpdateState()
    {

    }
}
