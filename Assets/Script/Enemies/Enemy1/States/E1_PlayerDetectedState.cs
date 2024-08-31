
public class E1_PlayerDetectedState : E_PlayerDetectedState
{
    private Enemy1 enemy;

    public E1_PlayerDetectedState(Entity etity, StateManager stateManager, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(etity, stateManager, animBoolName, stateData)
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

        if (performLongRangeAction)
        {
           // stateManager.ChangeEnemyState(enemy.ChargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            //stateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
