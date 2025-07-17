public class E2_AngryIdleState : E_IdleState
{
    private Enemy2 enemy;
    public E2_AngryIdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(enemy.AngryMoveState);
        }
    }

}
