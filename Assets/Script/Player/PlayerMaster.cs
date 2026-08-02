
using System.Collections;
using UnityEngine;


//ORIGINAL QUE TIENE QUE QUEDAR
[DefaultExecutionOrder(-1)]
public class PlayerMaster : Entity
{

    
    [SerializeField] public InventoryComponent _inventory;
    [SerializeField] public WeaponsHand weaponHand;
    private IInteractable _lastItemOnSigth;
    [SerializeField] private Camera _camera;
    [SerializeField]private Animator _animator;
    [SerializeField]
    private BuffManager manager;

    public delegate void StamChange(float NewStam, float MaxStam);
    public StamChange OnStaminaChanged = delegate { };

    public bool DamageInCd = false;
   


    private void Start()
    {
      
       
        GameManager.Instance.Player = this;
        
    }

    public override void ReduceStamina(float Cost)
    {
        _currentStamina -= Cost;
        if (_currentStamina < 0) _currentStamina = 0;
        _StaminaCount = _StaminaCD;
        OnStaminaChanged.Invoke(_currentStamina,  _maxStamina);
        
    }



    private void Update()
    {

        if (Camera.main != null)
        {
            Ray _ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit _hit;



            if (Physics.Raycast(_ray, out _hit, 5, LayerMask.GetMask("ItemCollisions")))
            {

                if (_hit.transform.gameObject.GetComponent<IInteractable>() == null) return;

                if (_lastItemOnSigth != _hit.transform.gameObject.GetComponent<IInteractable>())
                {

                    string mensaje = _hit.transform.gameObject.GetComponent<IInteractable>().interactMessage;
                    GameManager.Instance.Ui.IndicateInteractItem(mensaje);
                }

                _lastItemOnSigth = _hit.transform.gameObject.GetComponent<IInteractable>();
            }
            else
            {
                _lastItemOnSigth = null;
                GameManager.Instance.Ui.IndicateInteractItem(null, true);
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


        }
    }


    public void InteractPressed()
    {

        if (_lastItemOnSigth == null) return;
        _lastItemOnSigth.Interact(this);
    }


    public override void applyDamage(float damage, bool ApplyKnockback = false, float knockbackForce = 0, Transform KnockBackFrom = null)
    {
        if (DamageInCd) { return; }
        StartCoroutine(DamageCd());
        float reducedDamage = Mathf.Max(0, damage - _armor);
        Shake(0.15f, 0.1f);
        if (currentShieldedLife <= 0)
        {
            _currentLife -= reducedDamage;
        }
        else if (currentShieldedLife - reducedDamage < 0)
        {
            float remainingDamage = reducedDamage - currentShieldedLife;
            currentShieldedLife = 0;
            _currentLife -= remainingDamage;
        }
        else
        {
            currentShieldedLife -= reducedDamage;
        }
        BuffManager.Instance.TriggerOnPlayerHitted(this.gameObject);
        SoundManager.instance.PlayRandom(SoundTypes.Damaged);
        if (_currentLife <= 0)
        {
            Die();
        }
        if (ApplyKnockback)
        {
            Vector3 KBDir = this.transform.position - KnockBackFrom.position;
            this.gameObject.GetComponent<PlayerMovement>().ApplyKnockback(KBDir, knockbackForce);
        }

        OnHealthChanged.Invoke(_currentLife, _maxLife);
        //manager.TriggerOnPlayerHitted(this.gameObject);
    }

    public  override void Die()
    {
        GameManager.Instance.PlayerDied();
    }

    public override void Heal(float _healAmount)
    {
        base.Heal(_healAmount);
        OnHealthChanged.Invoke(_currentLife, _maxLife);
    }
    public void AddStamina(float Stam)
    {
        _currentStamina += Stam;
        if (_currentStamina > _maxStamina) _currentStamina = _maxStamina;
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
        return;
        _animator.SetTrigger("TakeWeapon");
      

    }

    private Vector3 originalPos;


    public void Shake(float duration, float magnitude)
    {
      
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        yield return null;
        print("HAY QUE REIMPLEMENTAR CAMERA SHAKE");
        /*
        originalPos = _camera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            _camera.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.x);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _camera.transform.localPosition = originalPos;
        */
    }
    





    private IEnumerator DamageCd()
    {
        DamageInCd = true;
        yield return new WaitForSeconds(_DamageCDTime);
        DamageInCd = false;
    }

}
