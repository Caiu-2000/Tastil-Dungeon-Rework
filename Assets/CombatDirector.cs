using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
    public static CombatDirector instance;


    public List<GoblinMele> goblinsMele = new List<GoblinMele>();

    void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
