
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  _contextIndicator;
    private PlayerMaster  Player;
    [SerializeField] private Image _lifeBar, _stamBar;
 
    [SerializeField] private List<Image> _hotbarIndicators = new List<Image>(3);
    [SerializeField] private Transform _hotbarSelector;
    private Transform firstPosition;


    private float LastHealtValure = 1;


    [SerializeField] Image LifeIndicatorEffect;
    private Material miMaterial;

    [SerializeField] private Image ParryIndicator;
    [SerializeField] private Image HitMarker;
    [SerializeField] private Image ParryMarker;
    [SerializeField] private Image critMarker;
    [SerializeField] private Image crossHair;


    private void Awake()
    {

        firstPosition = _hotbarSelector.transform;
    }


    private void Start()
    {
        GameManager.Instance.Ui= this;
        if (!miMaterial) miMaterial = LifeIndicatorEffect.material;
        miMaterial.SetFloat("_Intensity", 0.0f);
        
        Player = GameManager.Instance.Player;

        Player._inventory._secondHand.OnParryUpdated += UpdateParryCD;
        Player._inventory._secondHand.OnParriedSucces += ParriedSomething;
        if (GameManager.Instance.Player)
        {
            GameManager.Instance.Player.OnHealthChanged += UpdateLife;
            GameManager.Instance.Player.OnStaminaChanged += UpdateStam;
            GameManager.Instance.Player.OnHittconnected += HitConnected;
            GameManager.Instance.Player._inventory.OnWeaponChanged += UpdateHitmarkers;
        }

    }

    private void Update()
    {
       
        UpdateStam(Player._currentStamina , Player._maxStamina );

    }

    public void UpdateStam(float newvalue , float maxValue)
    {
        
        _stamBar.fillAmount = (newvalue / maxValue);
       
    }

    public void IndicateInteractItem(string mensaje = null, bool empty = false)
    {
        if(empty)
        {
            _contextIndicator.text = "";
            return;
        }
        if (mensaje != null) _contextIndicator.text = mensaje;
        else  _contextIndicator.text = "Apreta 'E' para interactuar";  

    }
    
    public void UpdateLife(float current , float max)
    {
        
        StopAllCoroutines();
        StartCoroutine(ChangeUILife(current , max));
    }

    public void updateHotbarItem(int _index , Item _newItem)
    {
        _hotbarIndicators[_index].sprite = _newItem.GetIcon();
        _hotbarIndicators[_index].color = new Color(1,1,1,1);
    }

    public void ClearIcon(int _index)
    {
        if (_index < 0 || _index > _hotbarIndicators.Count - 1) return;
        _hotbarIndicators[_index].sprite = null;
        _hotbarIndicators[_index].color = new Color(1,1,1,0);
    }

    public void UpdateHotbarPosition(int _index)
    {
        if (_index == -1)
        {
            _hotbarSelector.position = firstPosition.position;
            return;
        }
        _hotbarSelector.position = _hotbarIndicators[_index].transform.position;
    }


    private IEnumerator ChangeUILife(float curr , float max)
    {
        float ElapsedTime = 0.0f;
        
        float percentValue = curr / max;
        while ((ElapsedTime) < 1)
        {
            ElapsedTime += Time.deltaTime * 2;
            LastHealtValure = Mathf.Lerp(LastHealtValure, percentValue, ElapsedTime );

           
            if (!miMaterial) miMaterial = LifeIndicatorEffect.material;
            _lifeBar.fillAmount = (LastHealtValure );
            miMaterial.SetFloat("_Intensity", (1 - LastHealtValure));
            yield return null;
        }

        yield return null;
    }

    public void UpdateParryCD(float newPercentaje)
    {
        ParryIndicator.fillAmount = newPercentaje;
    }

    public void HitConnected(bool WasCrit = false) 
    {
        StartCoroutine(ShowAndHideSprite(HitMarker, 0.2f));
        if (WasCrit) StartCoroutine(ShowAndHideSprite(critMarker, 0.2f));
    
    }
    public void ParriedSomething() 
    {
        StartCoroutine(ShowAndHideSprite(ParryMarker, 0.2f));
    
    }

    private IEnumerator ShowAndHideSprite(Image sprite , float time)
    {
        sprite.color = Color.white;
        yield return new WaitForSeconds(time);
        
        sprite.color = new Color(0, 0, 0, 0);

    }

    public void UpdateHitmarkers(Weapon weapon)
    {
        
        HitMarker.sprite = weapon.hitmarker;
        critMarker.sprite = weapon.critmarker;
        crossHair.sprite = weapon.crosshair;
    }
}
