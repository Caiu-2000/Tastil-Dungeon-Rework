using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    [SerializeField] private InputAction _useAction, _movementAction, _lookAction, _attackAction , _interactAction , _blockAction, _jumpAtion , _rightClickAction;
    [SerializeField] private PlayerMaster _EntityController = null;
    //[SerializeField] private InventoryComponent _inventory;
    Vector2 _dir = Vector2.zero;

    private InputAction _reset , _close;


    private InventoryComponent _inventory;



    public delegate void AttacksDelegate();
    
    public delegate void JumpPress();
    public delegate void UseAction();
  

    public AttacksDelegate OnAttackPressed = delegate { };
    public AttacksDelegate OnAttackReleased = delegate { };
    public JumpPress OnJumpPress = delegate { };
    public UseAction OnUsePressed = delegate { };
    


    // Este todavia no se usa pero ya queda aca
    // public UseAction OnUseReleased = delegate { };
    
    private void Awake()
    {
        
        _movementAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _interactAction = InputSystem.actions.FindAction("Interact");
       
        _useAction = InputSystem.actions.FindAction("Use");
        _jumpAtion = InputSystem.actions.FindAction("Jump");
        _rightClickAction = InputSystem.actions.FindAction("SecondClick");
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;


        //PASAR A GAMEMANAGER
        /*
        _reset = InputSystem.actions.FindAction("Reset");
        _close = InputSystem.actions.FindAction("Close");*/
    }


    private void Update()
    {/*
        #region ControlesEscena
        if (_close.WasPressedThisFrame())
        {
            print("Se intento salir");
            Application.Quit();
        }
        if (_reset.WasPressedThisFrame()) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        #endregion
        */
        _dir = _movementAction.ReadValue<Vector2>();
        print(_movementAction.ReadValue<Vector2>());
        if(_interactAction.WasPressedThisFrame()) _EntityController.InteractPressed();
        

            Vector2 lookDir = _lookAction.ReadValue<Vector2>();
        _playerMovement.Rotate(lookDir);

        float BlockState = _blockAction.ReadValue<float>();


       

        if(_attackAction.WasPressedThisFrame())
        {
            OnAttackPressed?.Invoke();
        }
        if(_attackAction.WasReleasedThisFrame())
        {
            OnAttackReleased?.Invoke();
        }
        if(_jumpAtion.WasPressedThisFrame())
        {
            OnJumpPress?.Invoke();
        }
        if (_useAction.WasPressedThisFrame())
        {
            OnUsePressed?.Invoke();
        }
        if (_rightClickAction.WasPressedThisFrame())
        {
            
        }


        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            
            for (int i = 1; i <= 3; i++)
            {
             
                Key tecla = (Key)System.Enum.Parse(typeof(Key), "Digit" + i);

                if (Keyboard.current[tecla].wasPressedThisFrame)
                {
                    _inventory.ChangeSelection(i  - 1 );
                    break;
                }
            }
        }








    }

    private void FixedUpdate()
    {

        _playerMovement.Movement(new Vector3 (_dir.x,_dir.y,0));
    }
}



