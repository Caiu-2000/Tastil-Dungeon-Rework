
using UnityEngine;

public class GoblinMele : Enemy
{

    public StateMachine machine;
    void Start()
    {
        BaseStart();
        CombatDirector.instance.AddToList(this);
        print("Si se llamo aca");
        machine.Initialice(this, moveComp, _ai);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ApplyStun(float StunTime = 1)
    {
        print("Me parrearon");
        machine.ApplyStun(StunTime);
    }

    public override void Die()
    {
        machine.CharacterDied();
    }
    
}
