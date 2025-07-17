public class E1_MoveState : E_MoveState
{
    private Enemy1 enemy;

    public E1_MoveState(Entity etity, StateManager stateManager, string animBoolName, D_MoveState stateData,
        Enemy1 enemy) : base(etity, stateManager, animBoolName, stateData)
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