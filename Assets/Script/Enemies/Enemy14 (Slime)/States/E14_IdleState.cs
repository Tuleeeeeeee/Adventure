public class E14_IdleState : E_IdleState
{
    private Enemy14 enemy;

    public E14_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy14 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
