using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public Transform roomAnchor;
    [SerializeField] GameObject thisRoom;
    public bool firstRoom = true;

    void Awake()
    {
        firstRoom = true;
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
    public IEnumerator spawnRoom(GameObject roomPrefab, GameObject rewardPrefab)
    {
        try
        {
      
            thisRoom = Instantiate(roomPrefab, roomAnchor.position, Quaternion.identity);
            RoomController thisRoomController = thisRoom.GetComponent<RoomController>();
            thisRoomController.SetReward(rewardPrefab);
            CharacterController _cC = GameManager.Instance.Player.GetComponent<CharacterController>();
            _cC.enabled = false;
            GameManager.Instance.Player.transform.position = thisRoomController.GetEnterPosition().position;
            _cC.enabled = true;
            GameManager.Instance.Player.GetComponent<PlayerMaster>().ToggleCamera();
            GameManager.Instance.Player.GetComponent<PlayerMaster>().ToggleCamera();
        }
        catch (System.Exception e)
        {
         
            yield break;
        }
           
        yield return StartCoroutine(FadeController.Instance.UnFade());


    }
    public IEnumerator destroyRoom()
    {
        
        yield return StartCoroutine(FadeController.Instance.Fade());
        Destroy(thisRoom);
    }
    public IEnumerator TransitionToRoom(GameObject roomPrefab, GameObject rewardPrefab)
    {
        yield return StartCoroutine(destroyRoom());
        yield return StartCoroutine(spawnRoom(roomPrefab, rewardPrefab));
       
        thisRoom.GetComponent<RoomController>().ActivateSpawners();
    }
    public void setBool()
    {
        firstRoom = false;
    }
    public GameObject GetRoom()
    {
        return thisRoom;
    }
    public void SetRoomAndActualRoom(Transform roomAnchor, GameObject thisRoom)
    {
        this.thisRoom = thisRoom;
        this.roomAnchor = roomAnchor;
        firstRoom = true;
    }
}
