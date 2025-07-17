public class E9_InAirState : E_InAirState
{
    private Enemy9 enemy;
    public E9_InAirState(Entity entity, StateManager stateManager, string animBoolName, Enemy9 enemy) : base(entity, stateManager, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
      
        if (IsGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
        else
        {
            base.LogicUpdate();
        }
    }
}
