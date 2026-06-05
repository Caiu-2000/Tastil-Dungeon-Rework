using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] public Transform playerEnterPosition;
    public Transform GetEnterPosition() => playerEnterPosition;
}
