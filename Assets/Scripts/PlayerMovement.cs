using UnityEngine;

public class PlayerMovement : MovementComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float rotationY = 0f, _rotationX = 0f , rotationspeed = 25f ;
    [SerializeField] private Camera _camera;
    [SerializeField]private PlayerInput _inputComp;
    [SerializeField] private CharacterController Cc;
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
        Vector3 move = transform.forward * dir.y + transform.right * dir.x;

        move = move.normalized * speed * Time.fixedDeltaTime * force;
        Cc.SimpleMove(move);
    }
    public override void jump()
    {
        
        if (Cc.isGrounded)
        {
            
            Cc.Move(new Vector3(0, _jumpForce, 0));
        }
    }

    private void FixedUpdate()
    {
        if (Cc.isGrounded)
        {
            
        }
    }

}
