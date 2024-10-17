public class E2_MoveState : E_MoveState
{
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Stats stats;

    private Enemy2 enemy;
    public E2_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (Stats.CurrentHealth <= 100)
        {
            entity.Animator.SetTrigger("hit1");
            stateManager.ChangeEnemyState(enemy.AngryMoveState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
