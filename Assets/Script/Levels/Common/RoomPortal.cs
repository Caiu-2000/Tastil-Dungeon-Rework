using UnityEngine;

public class RoomPortal : MonoBehaviour, IInteractable
{
    public string interactMessage { get; set; } = "Start the adventure";
    [SerializeField] GameObject ui;
    [SerializeField] GameObject nextRoom;
    bool firstRoom = true;
    public void Interact(PlayerMaster _player = null)
    {
        if(ui.gameObject.activeSelf || firstRoom)
        {
            StartCoroutine(RoomManager.instance.TransitionToRoom(nextRoom));
            if (firstRoom)
            {
                firstRoom = false;
                interactMessage = "Go to the next room";
                //bere se la come
            }
        }
    }

   
}
