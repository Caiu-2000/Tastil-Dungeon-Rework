
using UnityEngine;
[RequireComponent  (typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5f,  force = 10f , _jumpForce;
    private Rigidbody Rb = null;
    
    protected Entity _entityController;

    [SerializeField] bool RespectGroundDamping = true;

    private void Awake()
    {
        if (!Rb) Rb = GetComponent<Rigidbody>();
        if (!_entityController) _entityController = GetComponent<Entity>();
    }




    public void Movement(Vector2 dir)
    {
        
        
        if(!_entityController._CanInputMovement || !CheckGrounded()) return;
        
        
        Vector3 move = transform.forward * dir.y + transform.right * dir.x;

        move = move.normalized * speed * Time.fixedDeltaTime * force;


        Rb.linearVelocity += move;

    }
    public virtual void Movement(Vector3 dir)
    {
        if (!_entityController._CanInputMovement) return;
        Vector3 move = transform.forward * dir.y + transform.right * dir.x;

        move = move.normalized * speed * Time.fixedDeltaTime * force;


        Rb.linearVelocity += new Vector3(move.x , 0 , move.z);

    }


    public void ApplyKnockback(Vector3 knockbackDir , float force)
    {
        Rb.linearVelocity = Vector3.zero;
        Vector3 move = knockbackDir.normalized * speed * Time.fixedDeltaTime * force;
        Rb.AddForce(move, ForceMode.Impulse);
    }

    public virtual void jump()
    {
        if (!CheckGrounded()) return;
        Rb.AddForce(new Vector3(0,_jumpForce,0), ForceMode.Impulse);
    }


    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed( float _newSpeed)
    {
        speed = _newSpeed;
    }

    public void ChangeDamping( float _newDamping)
    {
        Rb.linearDamping = _newDamping;
    }


    bool CheckGrounded()
    {
        float distance = 1.1f; // Slightly more than half the player's height
        return Physics.Raycast(transform.position, Vector3.down, distance);
    }


    private void FixedUpdate()
    {
        if (!RespectGroundDamping) 
        {
            return;
        }
        if (CheckGrounded())
        {
            Rb.linearDamping = 15;
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x , 0 , Rb.linearVelocity.z);
        }
        else
        {
            Rb.linearDamping = 0;

        }
    }


}
