
using Unity.VisualScripting;
using UnityEngine;


public class MeleWeapon : Weapon
{
    
    
    [SerializeField] private bool _canKnockback = true;
    [SerializeField] private int _maxCombo = 3, _currentCombo = 0;
    private float ComboCd = 3.0f, ComboCount = 0.0f;
    //Por ahora las separe por que puede que esto se defina por las animaciones para el peso 
    //Lo mismo con parriable
    //y el alcance se lo de con la colision en el prefab

    [SerializeField] private float _weigth = 1.0f, _reach = 1.0f;
    public bool _parriable = false;

    

    public bool _attacking = false,  _activeHitt = false;

    private void Update()
    {
        if (ComboCount  > 0)
        {
            ComboCount -= Time.deltaTime;
        }
        else
        {
            _currentCombo = 0;
        }
        
        if (!_equiped) return;
    }

    public override float TryAttack()
    {
        if (_attacking || !_equiped) return 0.0f;
        return Attack();
    }
    public override float Attack()
    {
        _currentCombo += 1;
        if (_currentCombo > _maxCombo) _currentCombo = 1;
        ComboCount = ComboCd;
        animator.SetInteger("AttackCombo", _currentCombo);
        animator.SetTrigger("Attack");
        _attacking = true;
        return _stamCost;
    }

    public void HittedSomething( GameObject Hittedhthing)
    {
        
        if (Hittedhthing.GetComponent<PlayerInput>() || !_attacking) return;

        Hittedhthing.GetComponent<Entity>().applyDamage(_damage , _canKnockback , _knockbackForce , this.transform);
        
    }

    public override Item PicItem(Entity _entity)
    {
        return this;
        // Reescribir segun que item se agarre 
    }
}
