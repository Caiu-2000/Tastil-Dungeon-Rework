using System.Collections.Generic;
using UnityEngine;
public enum ArenaProgressionMode
{
    FixedWaves,
    Endless,
    EndlessWithBossWaves
}
[CreateAssetMenu(fileName = "ArenaModeConfig", menuName = "Scriptable Objects/ArenaModeConfig")]
public class ArenaModeConfig : ScriptableObject
{
    public ArenaModeConfig progressionMode;
    [Header("Fixed Waves")] 
    public List<ArenaWaveData> fixedWaves;
    [Header("Endless")]
    public AnimationCurve enemyCountPerWave;
    public AnimationCurve difficultScalar;
    public List<GameObject> endlessEnemyPool;
    [Header("Boss Waves")]
    public int bossWaveInterval = 5;
    public List<GameObject> bossPool;
    [Header("Timing")]
    public float delayBetweenWaves = 20f;
}
