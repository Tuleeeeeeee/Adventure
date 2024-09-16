
public class E1_MoveState : E_MoveState
{
    private Enemy1 enemy;

    public E1_MoveState(Entity etity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(etity, stateManager, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}
