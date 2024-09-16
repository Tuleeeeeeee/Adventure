public class E4_IdleState : E_IdleState
{
    private Enemy4 enemy;

    public E4_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy4 enemy) : base(entity, stateManager, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMaxAgroRange)
        {
            stateManager.ChangeEnemyState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
