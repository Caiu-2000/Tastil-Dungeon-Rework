using UnityEngine;

public class BombHability : WeaponHability
{
    [SerializeField] protected  GlobalExplotion explotion;


    public override void RunHability()
    {
        Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * 2.5f + GameManager.Instance.Player.transform.position + new Vector3(0, 1.0f, 0);
        GlobalExplotion instance = Instantiate(explotion);
        instance.transform.position = AttackPos;
        instance.DontDamagePlayer = true;
    
    }

}
