using UnityEngine;

public class Fire : MonoBehaviour, IProjectile
{
    Transform targetPos;
    float timer = 3.0f;
    float timerCd;
    private void Update()
    {
        transform.position = targetPos.position;
        if (timer > timerCd)
            timerCd += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        print("TestFire");
        targetPos = target;
    }
}
