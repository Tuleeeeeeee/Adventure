
public class E4_PlayerDetectedState : E_PlayerDetectedState
{
    private Enemy4 enemy;
    public E4_PlayerDetectedState(Entity entity, StateManager stateManager, string animBoolName, D_PlayerDetected stateData, Enemy4 enemy) : base(entity, stateManager, animBoolName, stateData)
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
            stateManager.ChangeEnemyState(enemy.LongRangeAttack);
        }
        else if (!isPlayerInMinAgroRange)
        {
            stateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement?.Flip();
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
