public class E6_AbilityState : E_AbilityState
{
    private Enemy6 enemy;
    public E6_AbilityState(Entity entity, StateManager stateManager, string animBoolName, Enemy6 enemy) : base(entity, stateManager, animBoolName)
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

    /*    if (isAbilityDone)
        {*/
            if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
            {
                StateManager.ChangeEnemyState(enemy.IdleState);
            }
            else
            {
                StateManager.ChangeEnemyState(enemy.InAirState);
            }
       /* }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
