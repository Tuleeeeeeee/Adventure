public class E9_Run_UpState : EnemiesState
{
    private Enemy9 enemy;
    protected bool isGrounded;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public E9_Run_UpState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity, stateManager, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = CollisionSenses.Ground;
        isDetectingWall = CollisionSenses.WallFront;
        isDetectingLedge = CollisionSenses.LedgeVertical;
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
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isAnimationFinished && isGrounded)
        {
            stateManager.ChangeEnemyState(enemy.JumpState);
        }
        else if ((isDetectingWall /*|| !isDetectingLedge*/) && isGrounded)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}
