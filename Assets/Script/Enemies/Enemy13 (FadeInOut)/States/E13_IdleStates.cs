

public class E13_IdleStates : E_IdleState
{
    private Enemy13 enemy;

    public E13_IdleStates(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy13 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        enemy.DeactiveSprite();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
