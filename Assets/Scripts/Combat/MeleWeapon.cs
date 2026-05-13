using UnityEngine;


public class MeleWeapon : Weapon
{
    
    
    [SerializeField] private bool _canKnockback = true;
    [SerializeField] private int _maxCombo = 3, _currentCombo = 0;
    private float ComboCd = 3.0f, ComboCount = 0.0f;


    [SerializeField] private float _weigth = 1.0f, _reach = 1.0f;


    

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

    public override void TryAttack()
    {
        if (_attacking || !_equiped) return;
        if (ParentEntity._currentStamina >= _stamCost)
        {
            ParentEntity.ReduceStamina(_stamCost);

            _currentCombo += 1;
            if (_currentCombo > _maxCombo) _currentCombo = 1;
            ComboCount = ComboCd;

            _attacking = true;
        }
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
