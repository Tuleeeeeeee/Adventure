
public class E1_IdleState : E_IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity etity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(etity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
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
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
