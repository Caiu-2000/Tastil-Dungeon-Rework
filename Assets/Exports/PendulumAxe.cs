using UnityEngine;

public class PendulumAxe : MonoBehaviour
{
    [SerializeField] private float amplitude = 45f; 
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 swingAxis = Vector3.up; 
    private Quaternion baseRotation;

    private void Start()
    {
        baseRotation = transform.localRotation; 
    }

    private void Update()
    {
        float angle = amplitude * Mathf.Sin(Time.time * speed);
        transform.localRotation = baseRotation * Quaternion.AngleAxis(angle, swingAxis);
    }
}