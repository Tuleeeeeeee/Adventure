public class E3_PlayerDetectedState : E_PlayerDetectedState
{
    private Enemy3 enemy;
    public E3_PlayerDetectedState(Entity entity, StateManager stateManager, string animBoolName, D_PlayerDetected stateData, Enemy3 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (performCloseRangeAction)
        {
            StateManager.ChangeEnemyState(enemy.MeleeAtackState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.ChargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement?.Flip();
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
