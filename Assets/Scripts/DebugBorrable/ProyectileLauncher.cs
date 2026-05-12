using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ProyectileLauncher : MonoBehaviour
{
    [SerializeField] private Proyectile _proyectile;
    [SerializeField] private float _timeBetween = 0.5f;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private Transform _fireDirection;
    private void Start()
    {
        if (_firingPoint == null)
        {
            _firingPoint = this.transform;
        }
        StartCoroutine(FireProyectile());
    }

    IEnumerator FireProyectile()
    {
        while (true)
        {
            Proyectile _proyInstance = Instantiate(_proyectile);
            _proyInstance.transform.position = _firingPoint.position;
            _proyInstance.transform.LookAt(_fireDirection.position);
            
            yield return new WaitForSeconds(_timeBetween);
        }
    }

}
