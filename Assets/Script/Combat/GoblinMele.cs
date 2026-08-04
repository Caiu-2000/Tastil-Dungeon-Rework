using System.Linq;
using UnityEngine;

public class GoblinMele : Enemy
{

    public StateMachine machine;
    void Start()
    {
        CombatDirector.instance.goblinsMele.Append(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
