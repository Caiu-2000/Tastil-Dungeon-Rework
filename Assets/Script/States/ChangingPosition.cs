using UnityEngine;

public class ChangingPosition : State
{

    private Vector3 wantedPosition = Vector3.zero;
    [SerializeField] private float desiredDistance = 5.0f;
    [SerializeField] private LayerMask Mask;
    public override void StartState()
    {
       
        base.StartState();
        wantedPosition = GetNewObjective();
        ParentMachine.AiComponent.Changepos(wantedPosition);
        ParentMachine.AiComponent.ChangeEnabled(true);
    }

    public override void UpdateState()
    {
        print(ParentMachine.AiComponent.GetDistance());
        if (Vector3.Distance(this.transform.position, GameManager.Instance.Player.transform.position) >= 7)
        {
            ParentMachine.ChangeState(DefaultNextState);
        }
        if (ParentMachine.AiComponent.GetDistance() <= 0.7f)
        {
            StartState();
        }
    }

    

    private Vector3 GetNewObjective()
    {
        Vector3 direction = ( this.transform.position - GameManager.Instance.Player.transform.position).normalized;
        Vector3 FirstDesired = transform.position + direction * desiredDistance;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, desiredDistance , Mask))
        {
            FirstDesired = hit.point - direction * 0.5f;
        }


        return FirstDesired;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wantedPosition, 0.5f);
    }
}
