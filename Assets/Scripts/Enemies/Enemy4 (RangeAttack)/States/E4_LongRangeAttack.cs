using UnityEngine;

public class E4_LongRangeAttack : E_RangedAttackState
{
    private Enemy4 enemy;
    public E4_LongRangeAttack(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, Enemy4 enemy) : base(entity, stateManager, animBoolName, attackPosition, stateData)
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
