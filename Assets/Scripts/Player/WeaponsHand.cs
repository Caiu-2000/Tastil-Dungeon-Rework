using UnityEngine;
using UnityEngine.Windows;

public class WeaponsHand
{







    public void EquipWeapon(Item _newWeapon)
    {
        if (_equipedWeapon != null)
        {
            _equipedWeapon.DeactivateWeapon();
            _equipedWeapon.transform.SetParent(null);
            _equipedWeapon.ResetPosition();
            transform.localRotation = Quaternion.identity;
            _equipedWeapon = null;
        }
        _equipedWeapon = _newWeapon.GetComponent<Weapon>();
        _equipedWeapon.transform.SetParent(this, true);
        _equipedWeapon.transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _equipedWeapon._input = _input;
        _equipedWeapon.ActivateWeapon();
    }





}
