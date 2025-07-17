public class E6_IdleState : E_IdleState
{
    private Enemy6 enemy;
    private bool isGrounded;
    private bool isJumpAble;
    private bool isDetectingLedge;
    public E6_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy6 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = CollisionSenses.Ground;
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isJumpAble = Entity.CheckIfCanJump();
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
        if (isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
        if (isJumpAble && isGrounded && !isDetectingLedge)
        {
            StateManager.ChangeEnemyState(enemy.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
