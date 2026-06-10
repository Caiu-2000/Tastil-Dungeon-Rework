using UnityEngine;

// Clase base de la que heredan todos los efectos de cartas.
// Para crear un efecto nuevo: click derecho -> Create -> Cards -> Effects -> [NombreEfecto]
//
// NOTA: Cuando se integren los scripts de Combat y Player al proyecto,
// agregar PlayerMaster y Weapon como parametros de ApplyEffect.
public abstract class CardEffectBase : ScriptableObject
{
    public abstract void ApplyEffect();
}
