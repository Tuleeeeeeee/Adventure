public class E2_IdleState : E_IdleState
{
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Stats stats;

    private Enemy2 enemy;
    public E2_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
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

        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
        else if (Stats.CurrentHealth <= 100)
        {
            stateManager.ChangeEnemyState(enemy.AngryMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
