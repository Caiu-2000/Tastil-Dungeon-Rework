using System.Collections;
using UnityEngine;


public class TrowableItem : Item
{
    [SerializeField] private Trowable Tirableprefab;
    public override void Use()
    {
        StartCoroutine(Trowanim());
        _hand._animator.SetBool("Trowable" , false);
    }

    private IEnumerator Trowanim()
    {
        _hand.SetAnimationTrigger("Trow");

        yield return new WaitForSeconds(_useTime);
        SoundManager.instance.Play(SoundTypes.Trhow);
        var instancia = Instantiate(Tirableprefab);
        instancia._fromPlayer = true;
        instancia.transform.position = _inventory.SecondHandPosition();//GameManager.Player._Hand.transform.position;
        instancia.GetComponent<Rigidbody>().AddForce(GameManager.Instance.Player.transform.forward * 50, ForceMode.Impulse);
        Vector3 localSpinAxis = transform.right;
        instancia.GetComponent<Rigidbody>().AddTorque(localSpinAxis * 50, ForceMode.Impulse);
        Destroy(gameObject);
  
    }
}
