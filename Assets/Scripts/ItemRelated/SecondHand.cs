using System.Collections;
using UnityEngine;

public class ItemsHand : MonoBehaviour
{

    private Item _equipedItem;
    private bool _hotbarCd = false ;

    public void ParryPressed()
    {

    }
    

    public void ChangeItem(Item _newItem)
    {
        if (_hotbarCd) { return; }
        StartCoroutine(HotBarCd());
        if (_equipedItem != null) { 
        
            _equipedItem.transform.parent = null; 
            _equipedItem.transform.position = new Vector3(0, 1000, 0);
            _equipedItem._isPicked = false; 
            _equipedItem = null;
        }
        if (_newItem)
        {
            _newItem.transform.SetParent(this.transform, false);
            _newItem.transform.position = this.transform.position;
            _newItem.transform.rotation = this.transform.rotation;
            _newItem._isPicked = true;
            _equipedItem = _newItem;
        }
    }

    public Item GetItem()
    {
        return _equipedItem;
    }
    private IEnumerator HotBarCd()
    {
        _hotbarCd = true;
        yield return new WaitForSeconds(0.1f);
        _hotbarCd = false;
    }
}
