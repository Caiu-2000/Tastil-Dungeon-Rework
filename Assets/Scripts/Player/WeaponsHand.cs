using UnityEngine;
using UnityEngine.UIElements;


public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;
    [SerializeField] Transform WeaponPoint;


    public void Start()
    {
        SuscribeToInput();
    }


    public void EquipWeapon(Weapon _newWeapon)
    {
        if (_equipedWeapon != null)
        {
            _equipedWeapon.DeactivateWeapon();
            _equipedWeapon.transform.SetParent(null);
            _equipedWeapon.ResetPosition();
            transform.localRotation = Quaternion.identity;
            _equipedWeapon = null;
            _equipedWeapon.SetParentEntity(null);
      
        }
        
        _equipedWeapon = _newWeapon.GetComponent<Weapon>();
        _equipedWeapon.transform.SetParent(WeaponPoint, true);
        _equipedWeapon.transform.localPosition = Vector3.zero;
        _equipedWeapon.transform.localRotation = Quaternion.identity;
        _equipedWeapon.SetParentEntity(GameManager.Instance.GetPlayer());
        GameManager.Instance.Player.PickedNewWeapon(_equipedWeapon.WeaponID);
        _equipedWeapon.ActivateWeapon();



        
   


    }

    public virtual void AttackPressed()
    {
        if (_equipedWeapon)
        {
            
        }
    }
    public virtual void AttackReleased()
    {

    }


    private void SuscribeToInput()
    {
        GameManager.Instance.InputHandler.OnAttackPressed += AttackPressed;
        GameManager.Instance.InputHandler.OnAttackReleased += AttackReleased;
    }
    private void CancelInput()
    {
        GameManager.Instance.InputHandler.OnAttackPressed  -= AttackPressed;
        GameManager.Instance.InputHandler.OnAttackReleased -= AttackReleased;
    }
}
