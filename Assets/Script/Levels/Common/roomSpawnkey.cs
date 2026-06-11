using UnityEngine;
using UnityEngine.InputSystem;

public class roomSpawnkey :MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] GameObject reward;
    private void Update()
    {
        if(Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            StartCoroutine(RoomManager.instance.TransitionToRoom(room, reward));
        }
    }
}
