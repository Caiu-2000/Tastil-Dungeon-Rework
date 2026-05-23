using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "ProjectileRegistry", menuName = "Scriptable Objects/ProjectileRegistry")]
public class ProjectileRegistry : ScriptableObject
{
    [System.Serializable]
    public struct ProjectileEntry
    {
        public string key;
        public GameObject prefab;
    }

    public List<ProjectileEntry> entries;
    private Dictionary<string, GameObject> _lookup;

    public void Init() =>
        _lookup = entries.ToDictionary(e => e.key, e => e.prefab);

    public GameObject Get(string key) => _lookup[key];
}
