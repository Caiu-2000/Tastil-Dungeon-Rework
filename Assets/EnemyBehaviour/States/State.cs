
using UnityEngine;

//[System.Serializable]
public  class State : MonoBehaviour
{

    [SerializeField] protected string AnimationTrigger;
    [SerializeField] protected State DefaultNextState;
    [SerializeField] public bool IsPausable = true;
    protected StateMachine ParentMachine;
    protected Entity _controlledEntity;

    public void InitialiceState(StateMachine Machine , Entity entity)
    {
        ParentMachine = Machine;
        _controlledEntity = entity;
    }

    public virtual void StartState()
    {
        if (AnimationTrigger != null && _controlledEntity._SpriteAnimator != null) _controlledEntity._SpriteAnimator.SetTrigger(AnimationTrigger);
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
}
