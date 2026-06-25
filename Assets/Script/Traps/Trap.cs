using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> triggerComponents;
    [SerializeField] private List<MonoBehaviour> effectComponents;
    private bool hasFired;
}
