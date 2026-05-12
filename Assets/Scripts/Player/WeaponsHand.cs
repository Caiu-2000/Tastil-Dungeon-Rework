using UnityEngine;


public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;
    [SerializeField] private PlayerInput _playerInput;


    public void Start()
    {
        _playerInput = GameManager.Instance.GetInput();
    }


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
        _equipedWeapon.transform.SetParent(this.transform, true);
        _equipedWeapon.transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _equipedWeapon.ActivateWeapon();
    }

}
