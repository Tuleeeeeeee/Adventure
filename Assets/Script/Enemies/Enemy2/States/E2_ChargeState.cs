
public class E2_ChargeState : E_ChargeState
{
    private Enemy2 enemy;
    public E2_ChargeState(Entity entity, StateManager stateManager, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
        if (!isDectectingLedge || isDetectingWall)
        {
            stateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateManager.ChangeEnemyState(enemy.PlayerDetectedState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
