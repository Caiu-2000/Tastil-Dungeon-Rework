
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;



public class UiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  _contextIndicator;
    [SerializeField] private PlayerMaster  Player;
    [SerializeField] private Image _lifeBar, _stamBar;
 
    [SerializeField] private List<Image> _hotbarIndicators = new List<Image>(3);
    [SerializeField] private Transform _hotbarSelector;
    private Transform firstPosition;



    private void Awake()
    {
           if (!Player) Player = GetComponent<PlayerMaster>();

        firstPosition = _hotbarSelector.transform;
    }

    private void Update()
    {

        UpdateStam(Player._currentStamina , Player._maxStamina );
    }

    public void UpdateStam(float newvalue , float maxValue)
    {
        
        _stamBar.fillAmount = (newvalue / maxValue);
       
    }

    public void IndicateInteractItem(bool empty = false)
    {
        if(empty)
        {
            _contextIndicator.text = "";
            return;
        }
        _contextIndicator.text = "Apreta 'E' para interactuar";  

    }
    
    public void UpdateLife(float current , float max)
    {
        _lifeBar.fillAmount = (current /max);
       
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
