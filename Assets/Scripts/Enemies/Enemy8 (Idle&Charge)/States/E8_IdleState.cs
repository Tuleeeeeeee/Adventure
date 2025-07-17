public class E8_IdleState : E_IdleState
{
    private Enemy8 enemy;

    public E8_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData,
        Enemy8 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }
}