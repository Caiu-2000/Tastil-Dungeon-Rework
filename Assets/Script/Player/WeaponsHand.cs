using UnityEngine;



public class WeaponsHand : MonoBehaviour 
{
    private Weapon _equipedWeapon;
    [SerializeField] Transform WeaponPoint;
    [SerializeField] Animator WeaponAnimator;
    [SerializeField] private AnimatorOverrideController overrideController;

    void Start()
    {
   
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

        _equipedWeapon.ActivateWeapon(WeaponAnimator);
        
    }

    public void updateAnimations(WeaponAnimations anims)
    {
        if (anims == null) { return; }
        print(anims.TakeOut.name);
        overrideController["ACArreglado"] = anims.TakeOut;
        overrideController["Armature|Attack1Sword"] = anims.Attack1;
        overrideController["Armature|Attack2Sword"] = anims.Attack2;
        overrideController["Armature|Attack3Sword"] = anims.Attack3;
        overrideController["Armature|LoopPointing"] = anims.Loop;
        overrideController["Armature|ChargePointing"] = anims.Charge;
        overrideController["Armature|ReleasePointing"] = anims.Release;
        WeaponAnimator.SetTrigger("Desenfunde");

    }
  
}
