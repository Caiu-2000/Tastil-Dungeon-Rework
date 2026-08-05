using System.Linq;
using UnityEngine;

public class GoblinMele : Enemy
{

    public StateMachine machine;
    void Start()
    {
        BaseStart();
        CombatDirector.instance.AddToList(this);
        machine.Initialice(this, moveComp, _ai);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
