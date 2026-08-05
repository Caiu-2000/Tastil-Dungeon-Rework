using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyData> enemies;
}
[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnInterval;
}
