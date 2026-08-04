using System.Collections.Generic;
using System;
using UnityEngine;

public class ArenaEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private int aliveCount;
    public event Action OnAllEnemiesDefeated;
    public void SpawnWave(List<EnemySpawnEntry> enemies, float difficultyScalar =1f)
    {

    }
    private GameObject SpawnEnemyAt(GameObject enemyPrefab, Transform point, float difficulty)
    {
        var enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
        return enemy;
    }
    private void HandleEnemyDeath()
    {
        aliveCount--;
        if (aliveCount <= 0)
        {
            OnAllEnemiesDefeated();
        }
    }
}
