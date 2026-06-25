
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// HAY QUE ACORDARSE DE BORRAR TODOS LOS RETURN CUANDO AGREGUE LA UI DE NUEVO

public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  _contextIndicator;
    [SerializeField] private PlayerMaster  Player;
    [SerializeField] private Image _lifeBar, _stamBar;
 
    [SerializeField] private List<Image> _hotbarIndicators = new List<Image>(3);
    [SerializeField] private Transform _hotbarSelector;
    private Transform firstPosition;


    private float LastHealtValure = 1;


    [SerializeField] Image LifeIndicatorEffect;
    private Material miMaterial;

    [SerializeField] private Image ParryIndicator;



    private void Awake()
    {

        firstPosition = _hotbarSelector.transform;
    }


    private void Start()
    {
        if (!miMaterial) miMaterial = LifeIndicatorEffect.material;
        miMaterial.SetFloat("_Intensity", 0.0f);
        if (!Player) Player = GetComponent<PlayerMaster>();
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
    }

    public void ClearIcon(int _index)
    {
        if (_index < 0 || _index > _hotbarIndicators.Count - 1) return;
        _hotbarIndicators[_index].sprite = null;
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


}
