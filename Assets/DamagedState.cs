
using System.Collections;
using UnityEngine;

public class DamagedState : State
{
    public hittData HittData;
    private State PausedState;
    
    public override void StartState()
    {
        base.StartState();
        StartCoroutine(PassTime());
    }

    internal void SetHittData(hittData? attackData, State currentState)
    {
        HittData = attackData.Value;
        PausedState = currentState;

    }

    private IEnumerator PassTime()
    {
        print("damaged");
        yield return new WaitForSeconds(0.5f);
        ParentMachine.ChangeState(PausedState);
    }
}
