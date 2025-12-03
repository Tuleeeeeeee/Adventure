
public class PlayerGroundState : PlayerState
{
    protected int XInput;
    private bool _jumpInput;
    private bool _isGrounded;

    public PlayerGroundState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) :
        base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        _isGrounded = Player.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        Player.JumpState.ResetAmountOfJumpsLeft();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormInputX;
        _jumpInput = Player.InputHandler.JumpInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            StateManager.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateManager.ChangeState(Player.InAirState);
        }
    }
    
}