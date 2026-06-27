public interface IInteractable
{
    public string interactMessage { get; }
    public void Interact(PlayerMaster _player = null);
}