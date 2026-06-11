using UnityEngine;

public enum Archetype
{
    Tank,
    Berserker,
    Duelista,
    Piromano,
    Criomante
}

public enum BuffTarget
{
    Personaje,
    Arma
}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card")]
public class Card : ScriptableObject
{
    [Header("Informacion")]
    public new string name;
    [TextArea(3, 5)]
    public string description;
    public Sprite artwork;

    [Header("Clasificacion")]
    public Archetype archetype;
    public BuffTarget buffTarget;

    [Header("Efecto")]
    public CardEffectBase effect;
}
