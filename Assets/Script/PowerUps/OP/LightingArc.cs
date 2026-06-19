using UnityEngine;
using UnityEngine.InputSystem;

public class LightingArc : IOnHitBuff
{
    bool rightClick = false;
    Enemy doDamage;
    Transform electricVfx;
    public LightingArc(Transform vfx)
    {
        electricVfx = vfx;
    }
    public void ExecuteOnHit(GameObject enemy, BuffManager manager)
    {
        if (manager.GetRightClickBool())
        {
            electricVfx.gameObject.SetActive(true);
            
        }
        Collider[] enemies = Physics.OverlapSphere(enemy.transform.position, 15f);
        foreach (Collider collider in enemies)
        {
            if (collider.gameObject.CompareTag("Enemy") && collider.gameObject != enemy)
            {
                var lightingArc = manager.SpawnAndReturn("ElectricArc", enemy.transform.position, collider.gameObject.transform);
                ElectricArcVFX arc = lightingArc.GetComponentInChildren<ElectricArcVFX>();
                arc.Fire(enemy.transform.position, collider.gameObject.transform.position);
                doDamage = collider.gameObject.GetComponentInParent<Enemy>();
                if (doDamage == null) doDamage = collider.gameObject.GetComponent<Enemy>();
                doDamage.applyDamage(10f);
            }    
        }
    }
}
