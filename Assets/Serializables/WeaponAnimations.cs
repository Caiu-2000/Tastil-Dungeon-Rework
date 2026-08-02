using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAnimations", menuName = "Scriptable Objects/WeaponAnimations")]
public class WeaponAnimations : ScriptableObject
{
    public Animation TakeOut;
    public Animation Attack1;
    public Animation Attack2;
    public Animation Attack3;
    public Animation Charge;
    public Animation Release;
}
