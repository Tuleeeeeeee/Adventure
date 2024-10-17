using UnityEngine;

public class E12_SpikeOutIdleState : E_IdleState
{
    private Enemy12 enemy;
    public E12_SpikeOutIdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy12 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        core.GetComponentInChildren<Death>().enabled = false;
        enemy.UpdateDamageableArea(new Vector2(1.4f, 1.4f), new Vector3(0, -0.1f, 0));
       
    }

    public override void Exit()
    {
        base.Exit();
        enemy.UpdateDamageableArea(new Vector2(1.4f, 1.4f), new Vector3(0, 0, 0));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.SpikeInIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
