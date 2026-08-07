using System;
using UnityEngine;

public class BombProyectile : Proyectile
{
    private Vector3 targetPos;
    // Me robe el codigo de fede
    [SerializeField]private Rigidbody rb;
    private Entity parent;


    private void Launch()
    {
        Vector3 horizontalDir = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z).normalized;
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                          new Vector3(targetPos.x, 0, targetPos.z));

        float g = Physics.gravity.magnitude;
        float sinValue = Mathf.Clamp((distance * g) / (15 * 15), -1f, 1f);
        float angle = Mathf.Asin(sinValue) / 2f;

        float Vx = 15 * Mathf.Cos(angle);
        float Vy = 15 * Mathf.Sin(angle);

        rb.linearVelocity = (horizontalDir * Vx) + (Vector3.up * Vy);
    }

    public void SetObjective(Vector3 newpos)
    {
        targetPos = newpos;
        Launch();
    }

    private void Update()
    {
        
    }
    public override void Parry()
    {
        if (_wasRedirected ) return;
        print(parent);
        targetPos = parent.transform.position;
        _wasRedirected = true;
        _fromPlayer = true;
        Launch();

    }

    internal void SetParent(Enemy controlledEntity)
    {
        parent = controlledEntity;
    }
}
