using UnityEngine;

public class E_InAirState : EnemiesState
{
    protected bool isGrounded;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public E_InAirState(Entity entity, StateManager stateManager, string animBoolName) : base(entity, stateManager, animBoolName)
    {
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = CollisionSenses.Ground;
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;
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

        entity.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        entity.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
