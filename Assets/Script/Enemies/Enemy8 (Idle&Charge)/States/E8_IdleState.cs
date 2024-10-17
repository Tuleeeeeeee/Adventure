public class E8_IdleState : E_IdleState
{
    private Enemy8 enemy;

    public E8_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy8 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
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
