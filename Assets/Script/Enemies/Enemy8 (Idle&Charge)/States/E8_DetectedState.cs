
public class E8_DetectedState : E_PlayerDetectedState
{
    private Enemy8 enemy;

    public E8_DetectedState(Entity entity, StateManager stateManager, string animBoolName, D_PlayerDetected stateData, Enemy8 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
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
            stateManager.ChangeEnemyState(enemy.ChargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
