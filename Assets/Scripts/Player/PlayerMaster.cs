
using UnityEngine;
using UnityEngine.SceneManagement;

//ORIGINAL QUE TIENE QUE QUEDAR

public class PlayerMaster : Entity
{
    [SerializeField] private UiHandler Ui;
    [SerializeField] public InventoryComponent _inventory;
    [SerializeField] public WeaponsHand weaponHand;
    private IInteractable _lastItemOnSigth;
    [SerializeField] private Camera _camera;
    private Animator _animator;
    private BuffManager manager;
    private void Start()
    {
      
        _animator = GetComponent<Animator>();
        GameManager.Instance.Player = this;
    }

    public override void ReduceStamina(float Cost)
    {
        _currentStamina -= Cost;
        if (_currentStamina < 0) _currentStamina = 0;
        _StaminaCount = _StaminaCD;
        Ui.UpdateStam(_currentStamina,  _maxStamina);
        Ui.UpdateLife(_currentLife, _maxLife);
    }



    private void Update()
    {
        
        Ray _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit _hit;
        float _maxDistance = 100f;


        if (Physics.Raycast(_ray, out _hit, _maxDistance, LayerMask.GetMask("ItemCollisions")))
        {
            
            if(_hit.transform.gameObject.GetComponent<IInteractable>() == null) return;
        
            if (_lastItemOnSigth != _hit.transform.gameObject.GetComponent<IInteractable>())
            {
                Ui.IndicateInteractItem();
            }

            _lastItemOnSigth = _hit.transform.gameObject.GetComponent<IInteractable>();
        }
        else
        {
            _lastItemOnSigth = null;
            Ui.IndicateInteractItem(true);
        }
        if (_StaminaCount > 0)
        {
            _StaminaCount -= Time.deltaTime;
        }
        else if (_currentStamina < _maxStamina)
        {
            _currentStamina += _StaminaRegen * Time.deltaTime;
            if (_currentStamina > _maxStamina) _currentStamina = _maxStamina;
        }

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.red);
        
    }


    public void InteractPressed()
    {

        if (_lastItemOnSigth == null) return;
        _lastItemOnSigth.Interact(this);
    }


    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Die();
        }
        if (ApplyKnockback)
        {
            Vector3 KBDir = this.transform.position - KnockBackFrom.position;
            this.gameObject.GetComponent<PlayerMovement>().ApplyKnockback(KBDir, knockbackForce);
        }

        Ui.UpdateLife(_currentLife, _maxLife); 
        manager.TriggerOnPlayerHitted(this.gameObject);
    }

    public  override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void Heal(float _healAmount)
    {
        base.Heal(_healAmount);
        Ui.UpdateLife(_currentLife,_maxLife);
    }


    public Vector3 GetLookDretirection()
    {
        return _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)).direction;
    }



    public float GetLife()
    {
        return _currentLife;
    }
    public float GetMaxLife()
    {
        return _maxLife;
    }
    public void AddMaxLife(float amount)
    {
        _maxLife += amount;
        _currentLife = +amount;
    }


    public void SetAttackAnimation(string Trigger , int Combo)
    {
        _animator.SetTrigger(Trigger);
        _animator.SetInteger("AttackCounter" , Combo);
    }


    public void PickedNewWeapon(int Id)
    {
        _animator.SetTrigger("TakeWeapon");
        _animator.SetInteger("WeaponID", Id);

    }



}
