using UnityEngine;
[DefaultExecutionOrder(1)]
public class RoomPortal : MonoBehaviour, IInteractable
{
    [SerializeField] public string interactMessage { get; set; } = "Start the adventure";
    [SerializeField] GameObject ui;
    [SerializeField] GameObject nextRoom;
    bool firstRoom;
    private void Start()
    {
        firstRoom = RoomManager.instance.firstRoom;
        print(firstRoom);
        if (firstRoom == false)
        {
            interactMessage = "Go to the next room";
        }
    }
    public void Interact(PlayerMaster _player = null)
    {
        if(ui.gameObject.activeSelf || firstRoom)
        {
            StartCoroutine(RoomManager.instance.TransitionToRoom(nextRoom));
            if (firstRoom)
            {
                RoomManager.instance.setBool();
            }
        }
    }

   
}
