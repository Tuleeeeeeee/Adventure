public class E11_MoveState : E_MoveState
{
    private Enemy11 enemy;
    public E11_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy11 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}
