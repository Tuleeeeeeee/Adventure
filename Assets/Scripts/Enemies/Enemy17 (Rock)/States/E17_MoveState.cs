public class E17_MoveState : E_MoveState
{
    private Enemy17 enemy;

    public E17_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData,
        Enemy17 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isDetectingLedge || isDetectingWall)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}