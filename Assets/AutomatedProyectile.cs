using UnityEngine;

public class AutomatedProyectile : Proyectile
{


    private void Start()
    {
        _objective = GameManager.Instance.Player.transform;
    }
    private void Update()
    {
        if (!_wasRedirected) { transform.LookAt(_objective); }
        transform.position = transform.position + transform.forward * Time.deltaTime * _speed;
    }



}
