using UnityEngine;

public class E15_ChargeState : E_ChargeState
{
    private Enemy15 enemy;
    public E15_ChargeState(Entity entity, StateManager stateManager, string animBoolName, D_ChargeState stateData, Enemy15 enemy) : base(entity, stateManager, animBoolName, stateData)
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

        if(isDetectingWall)
        {
            enemy.Animator.SetTrigger("wall");
 
            stateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
