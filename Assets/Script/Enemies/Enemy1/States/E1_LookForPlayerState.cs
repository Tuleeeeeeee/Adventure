
public class E1_LookForPlayerState : E_LookForPlayerState
{
    private Enemy1 enemy;

    public E1_LookForPlayerState(Entity entity, StateManager stateManager, string animBoolName, D_LookForPlayer stateData, Enemy1 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
            //stateManager.ChangeEnemyState(enemy.PlayerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
