public class PlayerAbilityState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected ParticleEffect ParticleEffect { get => particleEffect ?? core.GetCoreComponent(ref particleEffect); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private ParticleEffect particleEffect;
    protected bool isAbilityDone;
    protected bool isGrounded;
    public PlayerAbilityState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)//
        {
            if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
            {
                stateManager.ChangeState(player.IdleState);
            }
            else
            {
                stateManager.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
