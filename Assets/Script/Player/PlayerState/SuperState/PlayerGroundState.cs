public class PlayerGroundState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected ParticleEffect ParticleEffect { get => particleEffect ?? core.GetCoreComponent(ref particleEffect); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private ParticleEffect particleEffect;

    protected int xInput;
    private bool jumpInput;
    private bool isGrounded;

    public PlayerGroundState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
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

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
