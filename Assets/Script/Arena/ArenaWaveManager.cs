using System;
using UnityEngine;

public class ArenaWaveManager : MonoBehaviour
{
    public enum ArenaState { Idle, SpawningWave, WaveActive, WaveCleared, GameOver, Victory}
    [SerializeField] private ArenaModeConfig config;
    [SerializeField] private ArenaEnemySpawner spawner;
    public ArenaState CurrentState { get; private set; }
    public int CurrentWave { get; private set; }
    public event Action<int> OnWaveStart;
    public event Action<int> OnWaveCleared;
    public event Action OnArenaVictory;
    public event Action OnArenaGameOver;
    private void OnEnable() => spawner.OnAllEnemiesDefeated += HandleWaveCleared;
    private void OnDisable() => spawner.OnAllEnemiesDefeated -= HandleWaveCleared;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HandleWaveCleared()
    {

    }
    public void ReportPlayerDeath()
    {
        CurrentState = ArenaState.GameOver;
        OnArenaGameOver?.Invoke();
    }
}
