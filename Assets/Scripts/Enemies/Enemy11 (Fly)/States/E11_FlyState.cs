using Tuleeeeee.CoreComponets;

public class E11_FlyState : E_FlyState
{
    private Health Stats
    {
        get => _stats ?? Core.GetCoreComponent(ref _stats);
    }

    private Health _stats;

    private Enemy11 _enemy;

    public E11_FlyState(Entity entity, StateManager stateManager, string animBoolName, D_FlyState stateData,
        Enemy11 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.SplitLeaf();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Stats.CurrentHealth <= 100)
        {
            Entity.Animator.SetTrigger("change");
            StateManager.ChangeEnemyState(_enemy.IdleState);
        }
    }
}