using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Scriptable Objects/BuffData")]
public class BuffData : ScriptableObject
{
    public string buffName;
    public float radius;
    public float damage;
    public float duration;
    public float tickInterval;
    public float heal;
    public float armor;
    public float durationTwo;
}
