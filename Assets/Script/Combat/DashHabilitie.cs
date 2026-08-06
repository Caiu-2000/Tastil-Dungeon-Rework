
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Entity;
using static UnityEngine.Rendering.DebugUI;

public class DashHabilitie : WeaponHability
{
    [SerializeField] protected float PreviousTime = 0.05f;
    [SerializeField] protected float DashSpeed = 3.0f;
    [SerializeField] protected float DashDuretion = 0.75f;
    [SerializeField] protected float Damage = 20.0f;
    private CharacterController Cc;
    private MeleWeapon Weapon;
    public override void InitialiceHability(Entity parent , Weapon weapon)
    {
        _entity = parent;
        Cc = parent.GetComponent<CharacterController>();
        if (weapon is  MeleWeapon) { Weapon = (MeleWeapon)weapon; }

    }
    public override void RunHability()
    {
        StartCoroutine(DashSecuence());
    }

    private IEnumerator DashSecuence()
    {
        yield return new WaitForSeconds(PreviousTime);
        Vector3 direction = GameManager.Instance.Player.GetLookDretirection();
        direction.y = 0;
        float elapsedTime = 0;
        List<IHittable> AlreadyHitted = new List<IHittable>();
        bool Connected = false, crit = false;

        while (elapsedTime < DashDuretion) 
        {
            elapsedTime += Time.deltaTime;
            Cc.Move(direction * DashSpeed * Time.deltaTime);

            if (elapsedTime > DashDuretion) { break; }
            
            Vector3 AttackPos = GameManager.Instance.GetPlayer().GetLookDretirection() * Weapon._reach + GameManager.Instance.Player.transform.position + new Vector3(0, Weapon._vertialOfsset, 0);

            
            Collider[] collisions = Physics.OverlapBox(AttackPos, new Vector3(Weapon._collSize, Weapon._collSize, Weapon._collSize), GameManager.Instance.Player.transform.rotation, LayerMask.GetMask("Hittable"));
            foreach (Collider Hitted in collisions)
            {
                IHittable hitable = Hitted.GetComponent<IHittable>();


                if (hitable == null) continue;
                if (AlreadyHitted.Contains(hitable)) continue;
                if (hitable.GetType() == GameManager.Instance.Player.GetType()) { continue; }
                Connected = true;
    
                

                    hitable.Hit(Damage, Weapon._canKnockback, 2.0f, GameManager.Instance.Player.transform);
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
