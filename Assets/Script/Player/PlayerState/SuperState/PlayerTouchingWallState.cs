public class PlayerTouchingWallState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int xInput;
    protected bool JumpInput;
    public PlayerTouchingWallState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
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
        isGrounded = CollisionSenses.Ground;
        isTouchingWall = CollisionSenses.WallFront;
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

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        if (JumpInput)
        {

            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateManager.ChangeState(player.WallJumpState);
        }
        else if (isGrounded)
        {
            stateManager.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || xInput != Movement.FacingDirection)
        {
            stateManager.ChangeState(player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
