using UnityEngine;
using UnityEngine.UIElements;


public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;



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
        _equipedWeapon.transform.SetParent(this.transform, true);
        _equipedWeapon.transform.localPosition = Vector3.zero;
        _equipedWeapon.transform.localRotation = Quaternion.identity;
        _equipedWeapon.SetParentEntity(GameManager.Instance.GetPlayer());

        _equipedWeapon.ActivateWeapon();


        GameManager.Instance.Player.PickedNewWeapon(0);
        //POR AHORA ES 0 POR QUE SOLO TENEMOS LA ESPADA


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
