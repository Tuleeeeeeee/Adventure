
public class E7_DetectedState : E_PlayerDetectedState
{
    private Enemy7 enemy;
    public E7_DetectedState(Entity entity, StateManager stateManager, string animBoolName, D_PlayerDetected stateData, Enemy7 enemy) : base(entity, stateManager, animBoolName, stateData)
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
            stateManager.ChangeEnemyState(enemy.RangeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
