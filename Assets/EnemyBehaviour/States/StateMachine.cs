
using UnityEngine;

using TMPro;
public class StateMachine :MonoBehaviour
{
    [SerializeReference] private State[] statesList;
    [SerializeReference] public State CurrentState;
    [SerializeReference] private State DefaultState;
    [SerializeField] private State DeathState;
    [SerializeField] private DamagedState _damaged;
    
    private Entity _entity;
    public MovementComponent _movement;
    internal  AiComponnent _ai;

    public delegate void Attack( Vector3 ObjPos);
    public Attack OnAttack = delegate {  };


    private State _pausedState;


    [SerializeField] private TMPro.TextMeshPro DebugText;


    internal void Initialice(Enemy enemy, MovementComponent movement , AiComponnent AI)
    {
        _entity = enemy;

        _entity.OnDamaged += Damaged;


        _movement = movement;
        _ai = AI;




        foreach (State state in statesList)
        {
            state.InitialiceState(this, _entity);
        }

        if (DefaultState != null)
        {
            CurrentState = DefaultState;
         
            CurrentState.StartState();
        }
    }
    void Update()
    {
        CurrentState.UpdateState();    
    }
    public void ForceInterrupt(State obligatoryState)
    {
        CurrentState.StopState();
        CurrentState = obligatoryState;
        CurrentState.StartState();
    }
    public void ChangeState(State state) 
    {
        if (CurrentState == DeathState) return;
        CurrentState.StopState();
        CurrentState = state;
        CurrentState.StartState();
       

    }

    public void CharacterDied()
    {
        foreach (State state in statesList) { state.StopAllCoroutines(); }
        ChangeState(DeathState);
    }

    public void CallAttack(Vector3 ObjPos )
    {
        OnAttack?.Invoke(ObjPos);
    }

    public void Damaged(Hitt attackData)
    {
        if (CurrentState.IsPausable)
        {
            
            CurrentState.PauseState();
            _damaged.HittData(attackData, CurrentState);
            _pausedState = CurrentState;
            CurrentState = _damaged;
            CurrentState.StartState();

        }
    }




}
