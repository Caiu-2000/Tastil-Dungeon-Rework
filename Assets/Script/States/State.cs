
using UnityEngine;

//[System.Serializable]
public  class State : MonoBehaviour
{

    [SerializeField] protected string AnimationTrigger;
    [SerializeField] protected State DefaultNextState;
    [SerializeField] public bool IsPausable = true;
    [SerializeField] protected StateMachine ParentMachine;
    protected Enemy _controlledEntity;
    

    public void InitialiceState(StateMachine Machine , Enemy entity)
    {
        print("HOLA SOY EL PUTO ESTADO Y ME LLEGA  " + Machine + entity + this.ToString());
        ParentMachine = Machine;
        _controlledEntity = entity;
    }

    public virtual void StartState()
    {
       // if (AnimationTrigger != null ) _controlledEntity._SpriteAnimator.SetTrigger(AnimationTrigger);
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
