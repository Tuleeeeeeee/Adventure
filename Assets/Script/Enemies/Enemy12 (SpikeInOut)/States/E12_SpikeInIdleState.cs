using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12_SpikeInIdleState : E_IdleState
{
    private Enemy12 enemy;
    public E12_SpikeInIdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy12 enemy) : base(entity, stateManager, animBoolName, stateData)
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
        core.GetComponentInChildren<Death>().enabled = true;
        enemy.UpdateDamageableArea(new Vector2(1.4f, 0.6f), new Vector3(0, -0.5f, 0));

    }

    public override void Exit()
    {
        base.Exit();
        enemy.UpdateDamageableArea(new Vector2(1.4f, 0.6f), new Vector3(0, 0, 0));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            stateManager.ChangeEnemyState(enemy.SpikeOutIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
