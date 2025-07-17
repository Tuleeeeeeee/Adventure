using Tuleeeeee.CoreComponets;

public class PlayerGroundState : PlayerState
{
    protected int XInput;
    private bool jumpInput;
    private bool isGrounded;

    public PlayerGroundState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) :
        base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        if (jumpInput && player.JumpState.CanJump())
        {
            stateManager.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateManager.ChangeState(player.InAirState);
        }
    }
    
}