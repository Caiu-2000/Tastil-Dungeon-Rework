using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class AiComponent : MonoBehaviour
{
    [SerializeField] Enemy _parentEnemy;
    [SerializeField] MovementComponent _movement;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] float _minDistance = 1f;

    private bool _enabled = false;
    private Transform _objectiveTransform;
    private bool fleeing = false;

    // Por ahora queda muy simple el movimiento pero quisiera meterle mas mano par hacerlo custom con el componente
    // de movimiento que estoy referenciando

    private void Update()
    {
        if ( !_enabled || _objectiveTransform == null)
        {
            return; 
        }
        float currentDistance = Vector3.Distance(this.transform.position, _objectiveTransform.position);

        if (currentDistance > _minDistance && fleeing != true)
        {
            _agent.SetDestination(_objectiveTransform.position);
            _parentEnemy.SetWalking(true);
        } 
        else if (fleeing == true)
        {
            Vector3 fleeDirection = (transform.position - _objectiveTransform.position).normalized;
            Vector3 fleeDestination = transform.position + fleeDirection;
            _agent.SetDestination(fleeDestination);
        }
        else
        {
            _parentEnemy.DistanceReached();
        }
    }

    public void ChangeObjective(Transform _newTransform)
    {

        _objectiveTransform = _newTransform;
    }
    public void ChangeEnabled(bool newState)
    {
     
        _enabled = newState;
    }

    public void TemporaryDisable(float time)
    {
        StartCoroutine(DisableCorroutine(time));
    }

    private IEnumerator DisableCorroutine(float Time)
    {
        _enabled = false;
        yield return new WaitForSeconds(Time);
        _enabled = true;
    }
    public Transform GetObjectiveTransform()
    {
        return _objectiveTransform;
    }
    public void Flee()
    {
        fleeing = true;
        StartCoroutine(DisableFlee());
    }
    private IEnumerator DisableFlee()
    {
        yield return new WaitForSeconds(3f);
        fleeing = false;
    }
}
