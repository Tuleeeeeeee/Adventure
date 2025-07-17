public class E9_Run_UpState : EnemiesState
{
    private Enemy9 enemy;
    protected bool isGrounded;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public E9_Run_UpState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity,
        stateManager, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = CollisionSenses.Ground;
        isDetectingWall = CollisionSenses.WallFront;
        isDetectingLedge = CollisionSenses.LedgeVertical;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (IsAnimationFinished && isGrounded)
        {
            StateManager.ChangeEnemyState(enemy.JumpState);
        }
        else if ((isDetectingWall /*|| !isDetectingLedge*/) && isGrounded)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}