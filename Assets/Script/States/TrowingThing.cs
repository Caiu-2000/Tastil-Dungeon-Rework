using System.Collections;
using UnityEngine;

public class TrowingThing : State
{
    [SerializeField] private BombProyectile proyectile;
    [SerializeField] private float TrowTime = 1.0f;

    public override void StartState()
    {
        base.StartState();
        StartCoroutine(TrowThing());
    }




    private IEnumerator TrowThing()
    {

        yield return new WaitForSeconds(TrowTime);
        BombProyectile bomb= Instantiate(proyectile, transform.position, Quaternion.identity);
       bomb.SetObjective(GameManager.Instance.Player.transform.position);
        bomb.SetParent(_controlledEntity);
        yield return new WaitForSeconds(1.5f);
    

        ParentMachine.ChangeState(DefaultNextState);
    
}
}
