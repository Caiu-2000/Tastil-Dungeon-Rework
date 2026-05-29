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

    // Por ahora queda muy simple el movimiento pero quisiera meterle mas mano par hacerlo custom con el componente
    // de movimiento que estoy referenciando

    private void Update()
    {
        if ( !_enabled || _objectiveTransform == null)
        {
            return; 
        }
        float currentDistance = Vector3.Distance(this.transform.position, _objectiveTransform.position);

        if (currentDistance > _minDistance)
        {
            _agent.SetDestination(_objectiveTransform.position);
            _parentEnemy.SetWalking(true);
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

}
