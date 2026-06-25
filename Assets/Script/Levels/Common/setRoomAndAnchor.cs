using UnityEngine;

public class setRoomAndAnchor : MonoBehaviour
{
    [SerializeField] Transform roomAnchor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RoomManager.instance.SetRoomAndActualRoom(roomAnchor, this.gameObject);  
    }

}
