
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//ORIGINAL QUE TIENE QUE QUEDAR
[DefaultExecutionOrder(-1)]
public class PlayerMaster : Entity
{

    [SerializeField] public UiHandler Ui;
    [SerializeField] public InventoryComponent _inventory;
    [SerializeField] public WeaponsHand weaponHand;
    private IInteractable _lastItemOnSigth;
    [SerializeField] private Camera _camera;
    private Animator _animator;
    [SerializeField]
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

                string mensaje = _hit.transform.gameObject.GetComponent<IInteractable>().interactMessage;
                Ui.IndicateInteractItem(mensaje);
            }

            _lastItemOnSigth = _hit.transform.gameObject.GetComponent<IInteractable>();
        }
        else
        {
            _lastItemOnSigth = null;
            Ui.IndicateInteractItem(null,true);
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
        //manager.TriggerOnPlayerHitted(this.gameObject);
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
        print($"Added {amount} of life");
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

    private Vector3 originalPos;


    public void Shake(float duration, float magnitude)
    {
      
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        originalPos = _camera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _camera.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.x);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _camera.transform.localPosition = originalPos;
    }
    
    public void ToggleCamera()
    {
        _camera.gameObject.SetActive(!_camera.gameObject.activeSelf);
    }


}
