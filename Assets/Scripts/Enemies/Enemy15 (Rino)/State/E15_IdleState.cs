public class E15_IdleState : E_IdleState
{
    private Enemy15 enemy;

    public E15_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData,
        Enemy15 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.ChargeState);
        }
    }
}