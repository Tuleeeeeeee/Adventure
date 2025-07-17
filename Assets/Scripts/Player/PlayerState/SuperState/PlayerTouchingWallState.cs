using Tuleeeeee.CoreComponets;

public class PlayerTouchingWallState : PlayerState
{
    protected Movement Movement
    {
        get => _movement ?? Core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? Core.GetCoreComponent(ref _collisionSenses);
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected int XInput;
    protected bool JumpInput;

    public PlayerTouchingWallState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName)
        : base(player, stateManager, playerData, animBoolName)
    {
    }
    

    public override void DoCheck()
    {
        base.DoCheck();
        IsGrounded = CollisionSenses.Ground;
        IsTouchingWall = CollisionSenses.WallFront;
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        if (JumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            stateManager.ChangeState(player.WallJumpState);
        }
        else if (IsGrounded)
        {
            stateManager.ChangeState(player.IdleState);
        }
        else if (!IsTouchingWall || XInput != Movement.FacingDirection)
        {
            stateManager.ChangeState(player.InAirState);
        }
    }
    
}