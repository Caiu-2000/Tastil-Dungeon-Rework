using UnityEngine;


abstract public class Item : MonoBehaviour , IInteractable
{
    [SerializeField] protected Item _ReplaceItem;
    private Vector2 _firstPosition;
    public bool _isPicked = false;
    // Por ahora voy a usar firsPosition para que los items vuelvan a su lugar y que 
    // la sala de pruebas se mantenga organizada
    public InventoryComponent _inventory;
    [SerializeField]private Sprite _itemIcon;
    [SerializeField] protected float _useTime = 1.0f;
    protected ItemsHand _hand;

    public enum ItemType
    {
        WeaponMele,
        WeaponRange,
        Consumable,
        Trowable,
        Item,
        Gold
    }
    [SerializeField]private ItemType Type;


    public string interactMessage => throw new System.NotImplementedException();

    private void Start()
    {
        _firstPosition = transform.position;
    }

    public virtual void Interact(PlayerMaster player = null)
    {
        player._inventory.AddItem(this);
    }



    public virtual Item PicItem( Entity _entity)
    {
        return this;
         // Reescribir segun que item se agarre 
    }

    public void ResetPosition()
    {
        transform.position = _firstPosition;
    }
    public ItemType GetItemType()
    {
        return Type;
    }

    public virtual void Use()
    {
        if(_ReplaceItem)
        {
            Item Replace = Instantiate(_ReplaceItem);
            _inventory.ReplaceItem(this, Replace);
            
        }
        Debug.Log("Se uso el item");
    }

    public void AddedToInventory()
    {
        transform.position = new Vector3(0, 15000, 0);
    }
    private void OnDestroy()
    {
        if (_inventory)
        {
            _inventory.RemoveItem(this);
        }

        GameManager.Instance.InputHandler.OnUsePressed -= Use;

    }

    public Sprite GetIcon()
    {
        return _itemIcon;
    }

    public void SetHand(ItemsHand newhand)
    {
        _hand = newhand;
    }


}
