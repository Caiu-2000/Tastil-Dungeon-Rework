using System.Collections;
using UnityEngine;

public class ItemsHand : MonoBehaviour
{

    private Item _equipedItem;
    private bool _hotbarCd = false;
    [SerializeField] Transform ItemPosition;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.InputHandler.OnUseItemPressed += TryToUse;
    }

    public void ParryPressed()
    {

    }


    public void ChangeItem(Item _newItem)
    {
        if (_hotbarCd) { return; }
        StartCoroutine(HotBarCd());
        if (_equipedItem != null)
        {
            _animator.SetTrigger("ChangeItem");
            _equipedItem.transform.parent = null;
            _equipedItem.transform.position = new Vector3(0, 1000, 0);
            _equipedItem._isPicked = false;
            _equipedItem = null;
        }
        else
        {
            _animator.SetTrigger("TakeItem");
        }
        if (_newItem)
        {
            _newItem.SetHand(this);
            _newItem.transform.SetParent(ItemPosition, false);
            _newItem.transform.position = ItemPosition.position;
            _newItem.transform.rotation = ItemPosition.rotation;
            _newItem._isPicked = true;
            _equipedItem = _newItem;

            _animator.SetBool("Trowable", (_newItem.GetItemType() == Item.ItemType.Trowable));
            

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



    public void SetAnimationTrigger(string trigger)
    {




        _animator.SetTrigger(trigger);
    }

    public void TryToUse()
    {
        if (_equipedItem != null)
        {
            _equipedItem.Use();
        }

    }
}
