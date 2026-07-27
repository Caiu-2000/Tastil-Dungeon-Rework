using UnityEngine;



public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;
    [SerializeField] Transform WeaponPoint;

    public void EquipWeapon(Weapon _newWeapon)
    {
        if (_equipedWeapon != null)
        {
            _equipedWeapon.DeactivateWeapon();
            _equipedWeapon.transform.SetParent(null);
            _equipedWeapon.ResetPosition();
            transform.localRotation = Quaternion.identity;       
            _equipedWeapon.SetParentEntity(null);
            _equipedWeapon = null;

        }
        
        _equipedWeapon = _newWeapon.GetComponent<Weapon>();
        _equipedWeapon.transform.SetParent(WeaponPoint, true);
        _equipedWeapon.transform.localPosition = Vector3.zero;
        _equipedWeapon.transform.localRotation = Quaternion.identity;
        _equipedWeapon.SetParentEntity(GameManager.Instance.GetPlayer());
        GameManager.Instance.Player.PickedNewWeapon(_equipedWeapon.WeaponID);
        _equipedWeapon.ActivateWeapon();

    }
  
}
