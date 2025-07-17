using Tuleeeeee.CoreComponets;

public class E2_MoveState : E_MoveState
{
    private Health Stats
    {
        get => _stats ?? Core.GetCoreComponent(ref _stats);
    }

    private Health _stats;

    private Enemy2 _enemy;

    public E2_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData,
        Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Stats.CurrentHealth <= 100)
        {
            Entity.Animator.SetTrigger("hit1");
            StateManager.ChangeEnemyState(_enemy.AngryMoveState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            _enemy.IdleState.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(_enemy.IdleState);
        }
    }
}