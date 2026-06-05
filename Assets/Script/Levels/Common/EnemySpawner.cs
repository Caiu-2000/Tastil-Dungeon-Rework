using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] RoomController roomController;
    [SerializeField] GameObject enemyPrefab;
    public void Activate()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        roomController.EnemySpawned(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Enemy>().SetRoomController(roomController);
    }
}
