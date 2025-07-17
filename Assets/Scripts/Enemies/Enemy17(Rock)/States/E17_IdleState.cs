public class E17_IdleState : E_IdleState
{
    private Enemy17 enemy;
    public E17_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy17 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

  
}
