using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesToSpawn", menuName = "Scriptable Objects/WavesToSpawn")]
public class WavesToSpawn : ScriptableObject
{
    public List<WaveData> waves;
}
