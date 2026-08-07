
using System;

using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
  
    [SerializeField] public ItemsHand _secondHand;
    [SerializeField] private WeaponsHand _weaponsHand;
    private Item[] ItemsInside = new Item[4];

    private int _currentSelection = 1;
    public delegate void WeaponUpdated(Weapon weapon);

    public WeaponUpdated OnWeaponChanged = delegate { };

    public void AddItem(Item _newItem)
    {
        if (_newItem._itemMesh != null)
        {
            _newItem._itemMesh.layer = 12;
            _newItem.RecursiveChangeLayer(_newItem._itemMesh );
        }
        else
        {
            _newItem.RecursiveChangeLayer(_newItem.gameObject, 12);
        }
        if (_newItem is Weapon weapon)
        {
            if (weapon._equiped) return;
            GameManager.Instance.CurrentWeapon = weapon;
            OnWeaponChanged?.Invoke(weapon);
            _weaponsHand.EquipWeapon(weapon);
            GameManager.Instance.Ui.changeWeapon(weapon._itemIcon);
            return;
        }
        if (_newItem is Item item)
        {
            for (int x = 0; x < ItemsInside.Length; x++)
            {
                if (ItemsInside[x] == null)
                {
                    ItemsInside[x] = item;
                    item._inventory = this;
                    item.AddedToInventory();
                    _secondHand.ChangeItem(item);

                    GameManager.Instance.Ui.updateHotbarItem(x, item);
                    ChangeSelection(x);
                    break;
                }
            }
        }
    }

    public void ChangeSelection(int newSelection)
    {
        if (newSelection < 0 || newSelection > ItemsInside.Length || newSelection == _currentSelection)
        {
            _secondHand.ChangeItem(null);
            _currentSelection = -1;
        }
        
        _currentSelection = newSelection;
        _secondHand.ChangeItem(ItemsInside[newSelection]);
        GameManager.Instance.Ui.UpdateHotbarPosition(_currentSelection);
    }


    public void UseItem()
    {
        //if (!(_currentSelection < 0 || _currentSelection >= ItemsInside.Count)) {

        if (_currentSelection < 0 || _currentSelection > ItemsInside.Length) return;
        if (ItemsInside[_currentSelection] == null) return;
         ItemsInside[_currentSelection].Use();
            
        //}
    }

    public void RemoveItem(Item _itemRef)
    {
        int _objIndex = Array.IndexOf(ItemsInside, _itemRef);
        if (_objIndex == -1) return;
        GameManager.Instance.Ui.ClearIcon(_objIndex);
        ItemsInside[_objIndex] = null;
    }
    public void RemoveItem(int index)
    {
       
        GameManager.Instance.Ui.ClearIcon(index);
        ItemsInside[index] = null;
    }

    public void ReplaceItem(Item oldItem, Item newItem)
    {
        int Index = Array.IndexOf(ItemsInside, oldItem);

        if (Index == -1) return;
        if (_secondHand.GetItem() == oldItem) _secondHand.ChangeItem(newItem);
        ItemsInside[Index] = newItem;
        newItem._inventory = this;
        GameManager.Instance.Ui.updateHotbarItem(Index, newItem);

    }
    public Vector3 SecondHandPosition()
    {
        return _secondHand.transform.position;
    }
}
