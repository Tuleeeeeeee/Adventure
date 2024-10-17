public class E9_AbilityState : E_AbilityState
{
    private Enemy9 enemy;

    public E9_AbilityState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity, stateManager, animBoolName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
        else
        {
            stateManager.ChangeEnemyState(enemy.InAirState);
        }
    }
}
