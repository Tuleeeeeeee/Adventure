public class E11_FlyState : E_FlyState
{
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    private Enemy11 enemy;
    public E11_FlyState(Entity entity, StateManager stateManager, string animBoolName, D_FlyState stateData, Enemy11 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        enemy.SplitLeaf();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Stats.CurrentHealth <= 100)
        {
            entity.Animator.SetTrigger("change");
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
