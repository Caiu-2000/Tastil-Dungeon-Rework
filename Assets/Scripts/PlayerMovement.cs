using UnityEngine;

public class PlayerMovement : MovementComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float rotationY = 0f, _rotationX = 0f , rotationspeed = 25f ;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInput _inputComp;
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
        base.Movement(dir);
    }
    public override void jump()
    {
        base.jump();
    }

}
