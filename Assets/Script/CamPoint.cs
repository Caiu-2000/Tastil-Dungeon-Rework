using UnityEngine;

public class CamPoint : MonoBehaviour
{

    void Update()
    {
        this.transform.position = GameManager.Instance.Player.transform.position;
        this.transform.position += new Vector3(0, 1, 0);
        Vector3 currentrotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(currentrotation.x, GameManager.Instance.Player.transform.eulerAngles.y, currentrotation.z);
    }
}
