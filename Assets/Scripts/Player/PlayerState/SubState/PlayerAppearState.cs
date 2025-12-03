public class PlayerAppearState : PlayerState
{
    public PlayerAppearState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        Player.StateManager.ChangeState(Player.IdleState);
    }
}
