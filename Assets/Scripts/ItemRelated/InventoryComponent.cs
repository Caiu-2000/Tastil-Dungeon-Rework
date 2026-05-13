
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private UiHandler Ui;
    [SerializeField] private ItemsHand _secondHand;
    [SerializeField] private WeaponsHand _weaponsHand;
    private Item[] ItemsInside = new Item[3];

    private int _currentSelection = 1;


    public void AddItem(Item _newItem)


    {
        print("Hasta aca se llegho bien");
        if (_newItem.GetItemType() == Item.ItemType.WeaponMele)
        {
            _weaponsHand.EquipWeapon(_newItem.GetComponent<MeleWeapon>());
            return;
        }
        for (int x = 0; x < ItemsInside.Length; x++) 
        {
            if (ItemsInside[x] == null)
            {
                ItemsInside[x] = _newItem;
                _newItem._inventory = this;
                _newItem.AddedToInventory();
                _secondHand.ChangeItem(_newItem);

                Ui.updateHotbarItem(x, _newItem);
                ChangeSelection(x);
                break;
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
        Ui.UpdateHotbarPosition(_currentSelection);
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
        Ui.ClearIcon(_objIndex);
        ItemsInside[_objIndex] = null;
    }
    public void RemoveItem(int index)
    {
       
        Ui.ClearIcon(index);
        ItemsInside[index] = null;
    }

    public void ReplaceItem(Item oldItem, Item newItem)
    {
        int Index = Array.IndexOf(ItemsInside, oldItem);

        if (Index == -1) return;
        if (_secondHand.GetItem() == oldItem) _secondHand.ChangeItem(newItem);
        ItemsInside[Index] = newItem;
        newItem._inventory = this;
        Ui.updateHotbarItem(Index, newItem);

    }
    public Vector3 SecondHandPosition()
    {
        return _secondHand.transform.position;
    }
}
