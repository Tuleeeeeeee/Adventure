public class E15_LookForPlayerState : E_LookForPlayerState
{
    private Enemy15 enemy;

    public E15_LookForPlayerState(Entity entity, StateManager stateManager, string animBoolName,
        D_LookForPlayer stateData, Enemy15 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        Entity.Rb.drag = 5;
      //  Movement.SetVelocityX(5f * -Movement.FacingDirection);
        Movement.SetVelocityY(5f);
    }

    public override void Exit()
    {
        base.Exit();
        Entity.Rb.drag = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAllTurnsTimeDone)
        {
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.ChargeState);
        }
    }
}