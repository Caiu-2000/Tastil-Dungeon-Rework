using UnityEngine;

public class Tp : MonoBehaviour
{
    [SerializeField] GameObject tpPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = tpPos.transform.position;
                cc.enabled = true;
            }
            else
            {
                other.transform.position = tpPos.transform.position;
            }
        }
    }
}