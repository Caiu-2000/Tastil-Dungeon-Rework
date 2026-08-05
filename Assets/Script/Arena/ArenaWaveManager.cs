using UnityEngine;
using System;
using System.Collections;

public class ArenaWaveManager : MonoBehaviour
{
    public enum ArenaState { Idle, SpawningWave, WaveActive, WaveCleared, GameOver, Victory}
    [SerializeField] private ArenaSpawner spawner;
    [SerializeField] private WavesToSpawn wavesConfig;
    public ArenaState CurrentState { get; private set;}
    public int CurrentWave { get; private set;}
    [SerializeField] float delayBetweenWaves;
    public event Action<int> OnWaveStart;
    public event Action<int> OnWaveCleared;
    public event Action OnArenaVictory;
    public event Action OnArenaGameOver;
    void Start()
    {
        spawner.OnAllEnemiesDefeated += HandleWaveCleared;
        CurrentState = ArenaState.Idle;
        StartCoroutine(WaveStartRoutine(delayBetweenWaves));
    }

    private IEnumerator WaveStartRoutine(float delay)
    { 
        yield return new WaitForSeconds(delay);
        StartNextWave();
    }
    private void StartNextWave()
    {
        CurrentWave++;
        CurrentState = ArenaState.SpawningWave;
        int waveIndex = CurrentWave - 1;
        if (waveIndex >= wavesConfig.waves.Count)
        {
            CurrentState = ArenaState.Victory;
            OnArenaVictory?.Invoke(); //does nothing for now i need to add the win script
            return;
        }
        spawner.SpawnWave(wavesConfig.waves[waveIndex].enemies);
        OnWaveStart?.Invoke(CurrentWave);
        CurrentState = ArenaState.WaveActive;
    }
    private void HandleWaveCleared()
    {
        if (CurrentState == ArenaState.Victory || CurrentState == ArenaState.GameOver)
            return;
        CurrentState = ArenaState.WaveCleared;
        OnWaveCleared?.Invoke(CurrentWave);
        StartCoroutine(WaveStartRoutine(delayBetweenWaves));
    }
    //for now it doesn't work as I didn't wire it to the player death
    public void ReportPlayerDeath()
    {
        StopAllCoroutines();
        CurrentState = ArenaState.GameOver;
        OnArenaGameOver?.Invoke();
    }
}
