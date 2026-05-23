
using UnityEngine;

public abstract  class Entity : MonoBehaviour , IHittable
{
                    //PENDIENTE CAMBIAR ESTO A PROTEGIDO O ALGO QUE LO PRIVATICE PARA TODO MENOS SUS 
                    //HERENCIAS
    [SerializeField] protected float _currentLife = 0, _maxLife = 100, _DamageCDTime = 0.25f;

    public bool _damCD = false;
    public bool _CanInputMovement = true;

    public float _maxStamina = 100.0f, _currentStamina = 0.0f;
    public float _StaminaCD = 1f , _StaminaCount = 0 , _StaminaRegen = 25f;
    
    private void Awake()
    { 
        _currentLife = _maxLife;
        _currentStamina = _maxStamina;
    }

    public virtual void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0.0f, Transform KnockBackFrom = null)
    {
        // PARCHE DE MIERDA 
        if (_currentLife == 0) _currentLife = _maxLife;
        _currentLife = _currentLife - damage;
    

        if (_currentLife <= 0)
        {
            Die();
        }
        if (ApplyKnockback)
        {
            print("Se llamo aplly knockbakc");
            Vector3 KBDir = this.transform.position - KnockBackFrom.position;

            if(this.gameObject.TryGetComponent(out MovementComponent movecomp))
            {
                movecomp.gameObject.GetComponent<MovementComponent>().ApplyKnockback(KBDir, knockbackForce * 1.5f);
            }
                
            
        }
    }

    private void Update()
    {
        if (_StaminaCount > 0) {
            _StaminaCount -= Time.deltaTime;
        }
        else if (_currentStamina < _maxStamina)
        {
            _currentStamina += _StaminaRegen * Time.deltaTime;
            if (_currentStamina > _maxStamina) _currentStamina = _maxStamina;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

 
    public virtual void ReduceStamina(float Cost)
    {
        _currentStamina -= Cost;
        if (_currentStamina <0) _currentStamina = 0;
    }

    public virtual void Heal(float _healAmount)
    {
        _currentLife += _healAmount;
        if (_currentLife > _maxLife) _currentLife = _maxLife;
    }

    public void Hit(float damage = 0, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        applyDamage(damage,ApplyKnockback,knockbackForce,KnockBackFrom);
    }
}