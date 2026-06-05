using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public Transform roomAnchor;
    [SerializeField] GameObject thisRoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator spawnRoom(GameObject roomPrefab)
    {
        thisRoom = Instantiate(roomPrefab, roomAnchor.position, Quaternion.identity);
        RoomController thisRoomController = thisRoom.GetComponent<RoomController>();
        CharacterController _cC=GameManager.Instance.Player.GetComponent<CharacterController>();
        _cC.enabled = false;
        GameManager.Instance.Player.transform.position = thisRoomController.GetEnterPosition().position;
        _cC.enabled = true;
        GameManager.Instance.Player.GetComponent<PlayerMaster>().ToggleCamera();
        GameManager.Instance.Player.GetComponent<PlayerMaster>().ToggleCamera();
        yield return StartCoroutine(FadeController.Instance.UnFade());


    }
    public IEnumerator destroyRoom()
    {
        yield return StartCoroutine(FadeController.Instance.Fade());
        Destroy(thisRoom);
    }
    public IEnumerator TransitionToRoom(GameObject roomPrefab)
    {
        yield return StartCoroutine(destroyRoom());
        yield return StartCoroutine(spawnRoom(roomPrefab));
        thisRoom.GetComponent<RoomController>().ActivateSpawners();
    }
}
