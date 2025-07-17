public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.Movement?.SetVelocityZero();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (XInput != 0 && !isExitingState)
        {
            stateManager.ChangeState(player.MoveState);
        }
    }
}
