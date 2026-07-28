using System.Collections;
using UnityEngine;

public class DamagedState : State
{
  
    public State Paused;




    public void HittData( State paused)
    {
      
        Paused = paused;
    }


    public override void StartState()
    {
        base.StartState();
        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        float elapsedTime = 0;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            // TODO codigo de movmiento
            //ParentMachine._movement.Move(GM.OppositeDirection(_hittData.AttackFrom , this.transform.position) * 3.0f);

            

            if (elapsedTime > 0.1f) break;

            yield return null;
        }

        ParentMachine.ChangeState(Paused);

    }



}
