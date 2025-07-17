using UnityEngine;

public class E3_MeleeAtackState : E_MeleeAttackState
{
    private Enemy3 enemy;

    public E3_MeleeAtackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition, 
        D_MeleeAttack stateData, Enemy3 enemy) : base(entity, stateManager, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
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

        if (IsAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                StateManager.ChangeEnemyState(enemy.PlayerDetectedState);
            }
            else
            {
                StateManager.ChangeEnemyState(enemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
