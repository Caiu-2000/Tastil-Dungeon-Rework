using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunHability : WeaponHability
{

    [SerializeField] private float PreviousTime = 0.5f;
    [SerializeField] private float StunDuration = 1.5f;
    [SerializeField] private float ApplyDuration = 0.2f;


    public override void RunHability()
    {
        StartCoroutine(StunSecuence());
    }


    private IEnumerator StunSecuence()
    {
        yield return new WaitForSeconds(PreviousTime);
        Vector3 direction = GameManager.Instance.Player.GetLookDretirection();
        direction.y = 0;
        float elapsedTime = 0;
        List<IStunable> AlreadyHitted = new List<IStunable>();
        bool Connected = false, crit = false;

        while (elapsedTime < ApplyDuration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * 1.5f + GameManager.Instance.Player.transform.position + new Vector3(0, 1.0f, 0);


            Collider[] collisions = Physics.OverlapBox(AttackPos, new Vector3(2,2,2), GameManager.Instance.Player.transform.rotation, LayerMask.GetMask("Hittable"));
            foreach (Collider Hitted in collisions)
            {
                IStunable hitable = Hitted.GetComponent<IStunable>();


                if (hitable == null) continue;
                if (AlreadyHitted.Contains(hitable)) continue;
                if (hitable.GetType() == GameManager.Instance.Player.GetType()) { continue; }
                Connected = true;



                hitable.CallStun(StunDuration);
                BuffManager.Instance.TriggerOnHit(Hitted.gameObject);

                AlreadyHitted.Add(hitable);
            }
            if (Connected)
            {
                GameManager.Instance.Player.OnHittconnected?.Invoke(crit);
            }
            yield return null;
        }

    }

}
