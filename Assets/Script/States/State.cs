
using System.Collections;
using UnityEngine;

//[System.Serializable]
public  class State : MonoBehaviour
{

    [SerializeField] protected string AnimationTrigger;
    [SerializeField] protected State DefaultNextState;
    [SerializeField] public bool IsPausable = true;
    [SerializeField] protected StateMachine ParentMachine;
    protected Enemy _controlledEntity;
    public bool IsOnCd = false;
    public float CdTime = 2.0f;

    public void InitialiceState(StateMachine Machine , Enemy entity)
    {

        ParentMachine = Machine;
        _controlledEntity = entity;
    }

    public virtual void StartState()
    {
       if (AnimationTrigger != null ) _controlledEntity._animator.SetTrigger(AnimationTrigger);
    }

    public virtual void StopState() 
    {
        StartCoroutine(CdCount());
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
    protected IEnumerator CdCount()
    {
        IsOnCd = true;
        yield return new WaitForSeconds(CdTime);
        IsOnCd = false;
    }
}
