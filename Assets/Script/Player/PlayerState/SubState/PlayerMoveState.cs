public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.CheckIfShouldFlip(xInput);
        Movement?.SetVelocityX(playerData.movementVelocity * xInput);
        ParticleEffect.DustMove();

        if (xInput == 0 && !isExitingState)
        {
            stateManager.ChangeState(player.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
