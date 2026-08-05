using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
//Este archivo deberia de spawnear enemigos en diferente partes
//Asi que tendria que almenos tener dos metodos, uno para llamar el spawn de una wave y otro para spawnear a cada enemygo
public class ArenaSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private int enemiesAlive;
    public event Action OnAllEnemiesDefeated;
    public void SpawnWave(List<EnemyData> enemies)
    {
        foreach(EnemyData enemy in enemies)
            StartCoroutine(SpawnDelay(enemy));
    }
    private GameObject SpawnEnemyAt(GameObject enemyPrefab, Transform point)
    {
        var enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
        enemiesAlive++;
        if (enemy.TryGetComponent<Entity>(out var entity))
            entity.OnEntityDead += HandleEnemyDeath;
        return enemy;
    }
    private IEnumerator SpawnDelay(EnemyData enemy)
    {
        for (int i = 0; i < enemy.count; i++)
        {
            var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            SpawnEnemyAt(enemy.enemyPrefab, point);
            if(enemy.spawnInterval > 0) yield return new WaitForSeconds(enemy.spawnInterval);
        }
    }
    private void HandleEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
            OnAllEnemiesDefeated?.Invoke();
    }
}
