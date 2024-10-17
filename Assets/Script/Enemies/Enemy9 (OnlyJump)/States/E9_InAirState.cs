public class E9_InAirState : E_InAirState
{
    private Enemy9 enemy;
    public E9_InAirState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity, stateManager, animBoolName)
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
            base.LogicUpdate();
        }
    }
}
