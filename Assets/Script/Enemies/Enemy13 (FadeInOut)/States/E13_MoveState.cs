public class E13_MoveState : E_MoveState
{
    private Enemy13 enemy;


    public E13_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy13 enemy) : base(entity, stateManager, animBoolName, stateData)
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

        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleStates.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleStates);
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
