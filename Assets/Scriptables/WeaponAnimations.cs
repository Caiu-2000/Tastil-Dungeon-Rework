using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAnimations", menuName = "Scriptable Objects/WeaponAnimations")]
public class WeaponAnimations : ScriptableObject
{
    public AnimationClip TakeOut;
    public AnimationClip Attack1;
    public AnimationClip Attack2;
    public AnimationClip Attack3;
    public AnimationClip Charge;
    public AnimationClip Release;
}
