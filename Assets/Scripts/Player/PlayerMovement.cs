
using UnityEngine;


public class PlayerMovement : MovementComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float rotationY = 0f, _rotationX = 0f , rotationspeed = 25f ;
    [SerializeField] private Camera _camera;
    [SerializeField]private PlayerInput _inputComp;
    [SerializeField] private CharacterController Cc;

    private Vector3 PlayerVelocity;

    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    private float verticalVelocity;

    public void Rotate(Vector2 lookdir)
    {
        rotationY += lookdir.x * rotationspeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        _rotationX += lookdir.y * -rotationspeed * Time.deltaTime; ;
        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }
    private void Awake()
    {

        if (!_entityController) _entityController = GetComponent<Entity>();
        if (_inputComp)
        {
            _inputComp.OnJumpPress += jump;
        }
    }

    public override void Movement(Vector3 dir)
    {

        if (!Cc.isGrounded)
        {

            verticalVelocity += gravity * Time.deltaTime;
        }


        Vector3 move = transform.forward * dir.y + transform.right * dir.x;
  
        
        move = move.normalized * speed * Time.fixedDeltaTime * force;

        Cc.SimpleMove(move);
        Cc.Move(new Vector3(0.0f, verticalVelocity, 0.0f));
    }
    public override void jump()
    {
      
        if (Cc.isGrounded)
        {
            verticalVelocity = jumpHeight;
     
            //Cc.Move(new Vector3(0, _jumpForce, 0));
        }
    }










    private void FixedUpdate()
    {
        if (Cc.isGrounded)
        {
            
        }
    }


    public override void ApplyKnockback(Vector3 knockbackDir, float force)
    {
        return;
    }

}
