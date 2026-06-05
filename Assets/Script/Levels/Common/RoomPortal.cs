using UnityEngine;

public class RoomPortal : MonoBehaviour, IInteractable
{
    public string interactMessage => "Press E to interact";
    [SerializeField] GameObject ui;
    [SerializeField] GameObject nextRoom;
    public void Interact(PlayerMaster _player = null)
    {
        print("Homosapien");
        if(ui.gameObject.activeSelf)
            StartCoroutine(RoomManager.instance.TransitionToRoom(nextRoom));
    }

   
}
