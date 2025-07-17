public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Movement?.CheckIfShouldFlip(XInput);

        if (XInput == 0 && !isExitingState)
        {
            stateManager.ChangeState(player.IdleState);
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Movement?.SetVelocityX(playerData.movementVelocity * XInput);
    }
}
