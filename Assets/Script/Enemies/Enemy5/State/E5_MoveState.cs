using UnityEngine;

public class E5_MoveState : E_MoveState
{
    private Enemy5 enemy;
    public E5_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy5 enemy) : base(entity, stateManager, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Movement?.SetVelocityY(Mathf.Sin(Time.time * stateData.movementSpeed) * 2);
        if (isDetectingWall)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}
