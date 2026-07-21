using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class EnemyHitCollision : MonoBehaviour
{
    /*Cuando se inicialice con duration -2 es por que van a ser permanentes, asi que con esa logica se van a usar para cuerpos que puedan meter daño
     varias veces si siguen en contacto con el jugador, esto se penso para el hongo */



    [SerializeField] public float HitDuration = 0.2f;
    [SerializeField] public Enemy parentEnemy;
    private bool playerInHitbox = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<PlayerMaster>())
        {
            if(HitDuration != -1.0f)
            {
                parentEnemy.HitConnectded(other);
                return;
            }
            playerInHitbox = true;
            StopAllCoroutines();
            StartCoroutine(DamagePlayer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMaster>()) { playerInHitbox = false; StopAllCoroutines(); }
    }

    private IEnumerator DamagePlayer()
    {
        parentEnemy.HitConnectded(GameManager.Instance.Player);
        yield return new WaitForSeconds(0.2f);
        if (playerInHitbox) { StartCoroutine(DamagePlayer()); }
    }

    private void Start()
    {
        if(HitDuration == -1.0f) return;
        
            Destroy(this.gameObject, HitDuration);    
    }

    public void ChangeDuration(float duration)
    {
        if (duration == -2.0f)
        {
            StartCoroutine(DestroyNextFrame());
            return;
        }
        Destroy(    this.gameObject, duration);
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return null;
        Destroy(this.gameObject);
    }

    

}
