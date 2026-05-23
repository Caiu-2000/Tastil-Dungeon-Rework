using UnityEngine;
using System.Collections.Generic;
public class FireRing : MonoBehaviour
{
    float timer = 1.75f;
    float timerCd;
    private void Update()
    {
        if (timerCd > timer)
            Destroy(gameObject);
        else
            timerCd+= Time.deltaTime;
    }
}
