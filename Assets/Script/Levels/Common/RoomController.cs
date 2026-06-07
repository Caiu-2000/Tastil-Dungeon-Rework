using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    [SerializeField] public Transform playerEnterPosition;
    [SerializeField] RewardUI rewardUI;
    public Transform GetEnterPosition() => playerEnterPosition;
    List<Enemy> enemies = new List<Enemy>();
    [SerializeField]List<EnemySpawner> spawners = new List<EnemySpawner>();
    bool spawnedEnemies = false;
    private void Awake()
    {
        spawners = new List<EnemySpawner>(GetComponentsInChildren<EnemySpawner>());
    }
    public void ActivateSpawners()
    {
        spawnedEnemies = true;
        foreach (EnemySpawner spawner in spawners)
            { spawner.Activate(); }
    }
    public void OnEnemyDied(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 && spawnedEnemies)
            rewardUI.Show();
    }
    public void EnemySpawned(Enemy enemy)
    {
        enemies.Add(enemy);
    }
}
