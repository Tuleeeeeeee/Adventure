
public class E2_LookForPlayerState : E_LookForPlayerState
{
    private Enemy2 enemy;
    public E2_LookForPlayerState(Entity entity, StateManager stateManager, string animBoolName, D_LookForPlayer stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateManager.ChangeEnemyState(enemy.PlayerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
