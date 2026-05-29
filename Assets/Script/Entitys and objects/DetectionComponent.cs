using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    [SerializeField] Enemy _master;
    [SerializeField] AiComponent _ai;
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerMaster>())
        {
            _ai.ChangeEnabled(true);
            _ai.ChangeObjective(other.GetComponent<PlayerMaster>().gameObject.transform);
        }
    }
}
