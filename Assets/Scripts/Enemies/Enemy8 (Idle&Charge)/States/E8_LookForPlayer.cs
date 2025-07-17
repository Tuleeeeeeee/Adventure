public class E8_LookForPlayer : E_LookForPlayerState
{
    private Enemy8 enemy;


    public E8_LookForPlayer(Entity entity, StateManager stateManager, string animBoolName, D_LookForPlayer stateData,
        Enemy8 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAllTurnsDone)
        {
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }
}