
public class E6_InAirState : E_InAirState
{
    private Enemy6 enemy;
    public E6_InAirState(Entity entity, StateManager stateManager, string animBoolName, Enemy6 enemy) : base(entity, stateManager, animBoolName)
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
      
    }

    public override void PhysicsUpdate()
    { 
        base.PhysicsUpdate();

        if (enemy.JumpState.CanJump() && !CollisionSenses.LedgeVertical && Entity.CheckIfCanJump())
        {
            StateManager.ChangeEnemyState(enemy.JumpState);
        }
        else if (IsGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
        else
        {
            base.LogicUpdate();
        }
    }
}
