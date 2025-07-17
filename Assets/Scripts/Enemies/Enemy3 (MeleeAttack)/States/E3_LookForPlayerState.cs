public class E3_LookForPlayerState : E_LookForPlayerState
{
    private Enemy3 enemy;
    public E3_LookForPlayerState(Entity entity, StateManager stateManager, string animBoolName, D_LookForPlayer stateData, Enemy3 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
