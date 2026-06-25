using System;
using UnityEngine;

public interface ITrapTrigger
{
    event Action OnActivate;
}

public interface ITrapEffect
{
    void Execute(Trap owner);
}
