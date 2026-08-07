public  class DeathState : State
{
    public override void StartState()
    {
        _controlledEntity._animator.SetTrigger("Death");
        Destroy(this.gameObject, 3.0f);
    }
}