public class E7_IdleState : E_IdleState
{
    private Enemy7 enemy;
    public E7_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy7 enemy) : base(entity, stateManager, animBoolName, stateData)
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
            StateManager.ChangeEnemyState(enemy.DetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
