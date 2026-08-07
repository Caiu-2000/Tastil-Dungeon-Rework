
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public AiComponent AiComponent;
   

    [SerializeReference] private State[] statesList;
    [SerializeReference] public State CurrentState;
    [SerializeReference] private State DefaultState;

    [SerializeField] DeathState deathState;
    [SerializeField] DamagedState damaged;
    [SerializeField] State ParryState;
    private Enemy _entity;
    public MovementComponent _movement;

    public Animator Modelanimator;
    
    public delegate void Attack(Vector3 ObjPos);
    public Attack OnAttack = delegate { };


    private State _pausedState;


    [SerializeField] private TMPro.TextMeshPro DebugText;


    internal void Initialice(Enemy enemy, MovementComponent movement, AiComponent AI)
    {
      
        _entity = enemy;

        _entity.OnDamaged += Damaged;


        _movement = movement;
        AiComponent = AI;




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
        if (CurrentState is  DeathState) return;
        CurrentState.StopState();
        CurrentState = state;
        CurrentState.StartState();


    }

    public void CharacterDied()
    {
        foreach (State state in statesList) {
            if (state is DamagedState) continue;
            state.StopAllCoroutines(); }
        damaged.PausedState = deathState;
        
        
    }

    public void CallAttack(Vector3 ObjPos)
    {
        OnAttack?.Invoke(ObjPos);
    }

    public void Damaged( hittData? attackData = null)
    {
        
        if (CurrentState.IsPausable)
        {

            CurrentState.PauseState();
            damaged.SetHittData(attackData, CurrentState);
            _pausedState = CurrentState;
           CurrentState = damaged;
            CurrentState.StartState();

        }
    }

    public void TryAttack()
    {

    }
    public void applyParry()
    {
        ForceInterrupt(ParryState);
    }

    public void ApplyStun(float TimeForStun)
    {
        
        CurrentState.StopAllCoroutines();
        CurrentState.StopState();

        damaged.SetHittData(new hittData(), DefaultState , true , TimeForStun);
        _pausedState = CurrentState;
        CurrentState = damaged;
        CurrentState.StartState();
    }

}
