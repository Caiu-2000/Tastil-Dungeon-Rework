
using System.Collections.Generic;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
    public static CombatDirector instance;


    public List<GoblinMele> goblinsMele = new List<GoblinMele>();

    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToList(GoblinMele mele)
    {
        goblinsMele.Add(mele);
    }
}
