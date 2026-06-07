using UnityEngine;
using UnityEngine.UI;

public class RoomPortal : MonoBehaviour, IInteractable
{
    public string interactMessage { get; set; } = "Start the adventure";
    [SerializeField] RewardUI rewardUI;
    [SerializeField] GameObject nextRoom;
    [SerializeField] Sprite[] rewardSprites;
    [SerializeField] GameObject[] rewardPrefabs;
    [SerializeField] Image rewardImage;
    bool firstRoom;
    GameObject selectedReward;

    private void Start()
    {
        firstRoom = RoomManager.instance.firstRoom;
        interactMessage = firstRoom ? "Start the adventure" : "Go to the next room";

        int index = Random.Range(0, rewardPrefabs.Length);
        selectedReward = rewardPrefabs[index];

        if (!firstRoom)
            rewardImage.sprite = rewardSprites[index];
    }

    public void Interact(PlayerMaster _player = null)
    {
        if (firstRoom || rewardUI.IsVisible())
        {
            RoomManager.instance.StartCoroutine(
                RoomManager.instance.TransitionToRoom(nextRoom, selectedReward)
            );
            if (firstRoom)
                RoomManager.instance.setBool();
        }
    }
}