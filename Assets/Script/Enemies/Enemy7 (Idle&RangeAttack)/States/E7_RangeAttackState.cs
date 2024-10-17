
using UnityEngine;

public class E7_RangeAttackState : E_RangedAttackState
{
    private Enemy7 enemy;
    public E7_RangeAttackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, Enemy7 enemy) : base(entity, stateManager, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateManager.ChangeEnemyState(enemy.DetectedState);
            }
            else
            {
                stateManager.ChangeEnemyState(enemy.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

   
}
