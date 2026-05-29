using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour, IProjectile
{
    Transform targetPos;
    float timer = 3.0f;
    float timerCd;
    private void Start()
    {
        StartCoroutine(Destroy());
    }
    private void Update()
    {
        transform.position = targetPos.position;
        
    }

    public void SetTarget(Transform target)
    {
        print("TestFire");
        targetPos = target;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
