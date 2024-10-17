public class E9_IdleState : E_IdleState
{
    private Enemy9 enemy;
    public E9_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy9 enemy) : base(entity, stateManager, animBoolName, stateData)
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
    /*    if (isDetectingWall)
        {
            SetFlipAfterIdle(true);
        }
*/
        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.Run_UpState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
