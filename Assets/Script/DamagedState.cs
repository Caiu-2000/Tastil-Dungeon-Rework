
using System.Collections;
using UnityEngine;

public class DamagedState : State
{
    public hittData HittData;
    private State PausedState;
    private bool WasStun;
    private float StunTime = 0.5f;
    public override void StartState()
    {
        base.StartState();
        StartCoroutine(PassTime());
    }

    internal void SetHittData(hittData? attackData, State currentState , bool WasStun = false , float stunTime = 1.0f)
    {
        HittData = attackData.Value;
        PausedState = currentState;
        if (WasStun)
        {
            StunTime = stunTime;
            this.WasStun = WasStun;

        }
        else
        {
            stunTime = 0.5f;
        }
    }

    private IEnumerator PassTime()
    {
        if (WasStun) { _controlledEntity._animator.SetTrigger("Stun"); }
        else { _controlledEntity._animator.SetTrigger("Damaged"); }
            yield return new WaitForSeconds(StunTime);
        ParentMachine.ChangeState(PausedState);
    }
}
