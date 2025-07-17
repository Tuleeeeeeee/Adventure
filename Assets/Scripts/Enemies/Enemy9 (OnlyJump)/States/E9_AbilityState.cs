public class E9_AbilityState : E_AbilityState
{
    private Enemy9 enemy;

    public E9_AbilityState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity,
        stateManager, animBoolName)
    {
        this.enemy = enemy;
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
        else
        {
            StateManager.ChangeEnemyState(enemy.InAirState);
        }
    }
}