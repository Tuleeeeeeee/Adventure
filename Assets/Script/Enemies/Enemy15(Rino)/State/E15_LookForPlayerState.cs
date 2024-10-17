public class E15_LookForPlayerState : E_LookForPlayerState
{
    private Enemy15 enemy;
    public E15_LookForPlayerState(Entity entity, StateManager stateManager, string animBoolName, D_LookForPlayer stateData, Enemy15 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        entity.RB.drag = 5;
        Movement.SetVelocityX(-5f * Movement.FacingDirection);
        Movement.SetVelocityY(5f);
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        entity.RB.drag = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAllTurnsTimeDone)
        {
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            stateManager.ChangeEnemyState(enemy.ChargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
