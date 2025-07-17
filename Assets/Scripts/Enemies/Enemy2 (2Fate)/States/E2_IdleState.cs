using Tuleeeeee.CoreComponets;

public class E2_IdleState : E_IdleState
{
    private Health Stats { get => _stats ?? Core.GetCoreComponent(ref _stats); }
    private Health _stats;

    private Enemy2 _enemy;
    public E2_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this._enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
         if (Stats.CurrentHealth <= 100)
        {
            Entity.Animator.SetTrigger("hit1");
            StateManager.ChangeEnemyState(_enemy.AngryMoveState);
        }
        else if (isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(_enemy.MoveState);
        }
       
    }
}
