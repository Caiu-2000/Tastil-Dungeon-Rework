
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// HAY QUE ACORDARSE DE BORRAR TODOS LOS RETURN CUANDO AGREGUE LA UI DE NUEVO

public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  _contextIndicator;
    private PlayerMaster  Player;
    [SerializeField] private Image _lifeBar, _stamBar;
 
    [SerializeField] private List<Image> _hotbarIndicators = new List<Image>(3);
    [SerializeField] private Transform _hotbarSelector;
    private Transform firstPosition;


    [SerializeField] Image LifeIndicatorEffect;
    private Material miMaterial;

    private void Awake()
    {
        return;
           if (!Player) Player = GetComponent<PlayerMaster>();

        firstPosition = _hotbarSelector.transform;
    }


    private void Start()
    {
        if (!miMaterial) miMaterial = LifeIndicatorEffect.material;
        miMaterial.SetFloat("_Intensity", 0.0f);
    }

    private void Update()
    {
        return;
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
        if(!miMaterial) miMaterial = LifeIndicatorEffect.material;
        _lifeBar.fillAmount = (current /max);
        miMaterial.SetFloat("_Intensity", (1 - _lifeBar.fillAmount));
       
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

}
