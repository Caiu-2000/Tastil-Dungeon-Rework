using System.Collections;

using UnityEngine;


public class Proyectile : MonoBehaviour , IParryable
{
    [SerializeField] public bool _fromPlayer = false;

    protected Transform _objective;
    [SerializeField] public float _weight, _speed = 1.0f , _damage = 50.0f;

    [SerializeField] private float _timeToAutoDelete = 10.0f;
    protected bool _wasRedirected = false;



    private void Start()
    {
       if (_objective) this.transform.LookAt(_objective.position);
        StartCoroutine(CountDestroy());
    }

    private void Update()
    {
        transform.position = transform.position + transform.forward *Time.deltaTime * _speed - new Vector3(0,_weight * Time.deltaTime,0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        IHittable hittable;
        if(collision.gameObject.TryGetComponent(out hittable))
        {
            if (hittable.GetType() == GameManager.Instance.Player.GetType())
            {
                if(_fromPlayer)
                {
                    return;
                }
            }
            else if (!_fromPlayer && hittable is Enemy) { return; }
            CallDestroy();
            hittable.Hit(_damage);
        }


    }

    protected virtual void CallDestroy()
    {
        Destroy(this.gameObject);
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

    public virtual void Parry()
    {
     
        RedirectHit(GameManager.Instance.Player.GetLookDretirection());
    }

    public void ChangeDirection(Vector3 direction)
    {
        transform.LookAt(this.transform.position  + direction);
    }


}
