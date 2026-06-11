using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    //---------------------Positions---------------------
    [SerializeField] public Transform playerEnterPosition;
    public Transform GetEnterPosition() => playerEnterPosition;
    [SerializeField] List<EnemySpawner> spawners = new List<EnemySpawner>();
    [SerializeField] Transform rewardSpawnPosition;
    //---------------------GameObjects-------------------
    [SerializeField] RewardUI[] rewardUI;
    List<Enemy> enemies = new List<Enemy>();
    bool spawnedEnemies = false;
    GameObject pendingReward;

    //---------------------Methods-----------------------
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
            SpawnReward();
    }
    public void EnemySpawned(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void SpawnReward()
    {
        if (pendingReward == null) return;
        Instantiate(pendingReward, rewardSpawnPosition.position, Quaternion.identity);
    }
    public void SetReward(GameObject reward)
    {
        pendingReward = reward;
    }
    public void PotionInteracted()
    {
        foreach (RewardUI rewardPanel in rewardUI)
            rewardPanel.Show();
    }
}
