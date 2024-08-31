
public class E1_IdleState : E_IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity etity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(etity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (isPlayerInMinAgroRange)
        {
          //  stateManager.ChangeEnemyState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
