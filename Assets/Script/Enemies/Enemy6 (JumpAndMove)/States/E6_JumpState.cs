public class E6_JumpState : E_JumpState
{
    private Enemy6 enemy;

    public E6_JumpState(Entity entity, StateManager stateManager, string animBoolName, D_JumpState stateData, Enemy6 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        if (isAbilityDone)
        {
            stateManager.ChangeEnemyState(enemy.AbilityState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
