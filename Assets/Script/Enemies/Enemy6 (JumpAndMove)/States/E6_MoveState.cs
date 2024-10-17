public class E6_MoveState : E_MoveState
{
    private Enemy6 enemy;
    private bool isGrounded;
    private bool isJumpAble;
    public E6_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy6 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = CollisionSenses.Ground;
        isJumpAble = entity.CheckIfCanJump();
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
        if (!isDetectingLedge && isJumpAble && isGrounded)
        {
            stateManager.ChangeEnemyState(enemy.JumpState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}
