using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArenaWaveData", menuName = "Scriptable Objects/ArenaWaveData")]
public class ArenaWaveData : ScriptableObject
{
    public List<EnemySpawnEntry> enemies;
}

