using UnityEngine;

public class TimerChoto : MonoBehaviour, IProjectile
{
    float Timer = 3f;
    float timerCd = 0;
    Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        //if (timerCd >= Timer)
        //    timerCd++;
        //else
        //    Destroy(gameObject);
    }
}   
