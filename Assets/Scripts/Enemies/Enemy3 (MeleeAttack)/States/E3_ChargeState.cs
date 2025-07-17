public class E3_ChargeState : E_ChargeState
{
    private Enemy3 enemy;

    public E3_ChargeState(Entity etity, StateManager stateManager, string animBoolName, D_ChargeState stateData, Enemy3 enemy) : base(etity, stateManager, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            StateManager.ChangeEnemyState(enemy.MeleeAtackState);
        }
        else if (!isDectectingLedge || isDetectingWall)
        {
            StateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                StateManager.ChangeEnemyState(enemy.PlayerDetectedState);
            }
            else
            {
                StateManager.ChangeEnemyState(enemy.LookForPlayerState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
