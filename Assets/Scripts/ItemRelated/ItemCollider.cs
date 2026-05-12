using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class ItemCollider : MonoBehaviour , IInteractable
{
    [SerializeField] public Item ParentItem;
    [SerializeField] private bool IsPicked = false;

    public string interactMessage => throw new System.NotImplementedException();

    public void Interact(PlayerMaster _player = null)
    {
       
    }

    public Item TrytoPickUp(Entity _entiy)
    {
        return ParentItem.PicItem(_entiy);
    }
}
