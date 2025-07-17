using UnityEngine;

public class E_InAirState : EnemiesState
{
    protected bool IsGrounded;
    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;
    public E_InAirState(Entity entity, StateManager stateManager, string animBoolName) : base(entity, stateManager, animBoolName)
    {
    }
    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = CollisionSenses.Ground;
        IsDetectingLedge = CollisionSenses.LedgeVertical;
        IsDetectingWall = CollisionSenses.WallFront;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Entity.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        Entity.Animator.SetFloat("xVelocity", Mathf.Abs((float)Movement.CurrentVelocity.x));
    }
}
