using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class ElectricArcVFX : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1000f;
    [SerializeField] private Transform pos1Transform;
    [SerializeField] private Transform pos2Transform;
    [SerializeField] private Transform pos3Transform;
    [SerializeField] private Transform pos4Transform;

    private VisualEffect vfx;

    private void Awake()
    {
        vfx = GetComponentInChildren<VisualEffect>();
    }

    public void Fire(Vector3 spawnerPos, Vector3 receiverPos)
    {
        pos1Transform.position = spawnerPos;
        pos4Transform.position = receiverPos;
        vfx.Play();

        Destroy(gameObject, lifeTime);
    }
}