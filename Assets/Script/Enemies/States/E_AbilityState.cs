public class E_AbilityState : EnemiesState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    protected bool isDetectingWall;
    public E_AbilityState(Entity entity, StateManager stateManager, string animBoolName) : base(entity, stateManager, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = CollisionSenses.WallFront;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
