using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Proyectile : MonoBehaviour
{
    [SerializeField] public bool _fromPlayer = false;
    private Vector3 _direction;
    [SerializeField] private Transform _objective;
    [SerializeField] public float _weight, _speed = 1.0f , _damage = 50.0f;
    [SerializeField] private bool _canKnockback = false;
    [SerializeField] private float _timeToAutoDelete = 10.0f;
    private bool _wasRedirected = false;
    
    private float _gravity = 0.0f;
    [SerializeField] private GameObject _objectModel;


    private void Start()
    {
       if (_objective) this.transform.LookAt(_objective.position);
        StartCoroutine(CountDestroy());
    }

    private void Update()
    {
        transform.position = transform.position + transform.forward *Time.deltaTime * _speed;
    }


    //No se si esto sera bueno o no pero bueno quedara preguntar despues
    //ESTO ESTA PENDIENTE A REWORKEAR CON INTERFACES 

    private void OnTriggerEnter(Collider collision)
    {
       
        if (collision.gameObject.CompareTag("Shield"))
        {
            collision.GetComponent<Shield>().BlockProyectile(this , _damage);
           
        }
        if (collision.gameObject.CompareTag("Player") && !_fromPlayer)
        {
            collision.gameObject.GetComponent<PlayerMaster>().applyDamage(_damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && _fromPlayer)
        {
            collision.gameObject.GetComponent<Entity>().applyDamage(_damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<BreacableComponent>() && _fromPlayer)
        {
            collision.gameObject.GetComponent<BreacableComponent>().Breack();
            Destroy(gameObject);
        }

         
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
            {
            other.GetComponent<Shield>().passTrough(this);
        }
    }



    private void Hitted()
    {

    }
    public void RedirectHit()
    {
        if (_wasRedirected) return;
        _wasRedirected = true;
        transform.LookAt(transform.forward * -1.0f);
    }
    public void RedirectHit(Vector3 _AimedTo)
    {
        if (_wasRedirected) return;
        _wasRedirected = true;
        transform.LookAt(_AimedTo + transform.position);
        _fromPlayer = true; 


    }
    public void SetObjective(Transform newObjective)
    {
        _objective = newObjective;
        this.transform.LookAt(_objective.position);
    }

    IEnumerator CountDestroy()
    {
        yield return new WaitForSeconds(_timeToAutoDelete);
        Destroy(gameObject);
    }
}
