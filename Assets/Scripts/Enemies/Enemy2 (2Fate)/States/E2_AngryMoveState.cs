public class E2_AngryMoveState : E_MoveState
{
    private Enemy2 enemy;
    public E2_AngryMoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.AngryIdleState.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(enemy.AngryIdleState);
        }
    }

 
}
