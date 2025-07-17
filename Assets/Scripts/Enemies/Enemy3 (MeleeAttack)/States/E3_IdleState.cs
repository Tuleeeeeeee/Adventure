public class E3_IdleState : E_IdleState
{
    private Enemy3 enemy;
    public E3_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy3 enemy) : base(entity, stateManager, animBoolName, stateData)
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

        if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
