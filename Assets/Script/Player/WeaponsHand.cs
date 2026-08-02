using UnityEngine;



public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;
    [SerializeField] Transform WeaponPoint;
    [SerializeField] Animator WeaponAnimator;
    [SerializeField] private AnimatorOverrideController overrideController;

    void Start()
    {
        WeaponAnimator = GetComponent<Animator>();

        WeaponAnimator.runtimeAnimatorController = overrideController;
        print(WeaponAnimator.runtimeAnimatorController.name);
    }


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

        updateAnimations(_equipedWeapon.GetAnimations());

        _equipedWeapon.ActivateWeapon();
        
    }

    public void updateAnimations(WeaponAnimations anims)
    {
        print(anims.TakeOut.name);
        overrideController["ACArreglado"] = anims.TakeOut;
        WeaponAnimator.SetTrigger("Desenfunde");

    }
  
}
