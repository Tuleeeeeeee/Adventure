using UnityEngine;

public class E_AttackState : EnemiesState
{
    protected Transform attackPosition;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    public E_AttackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition) : base(entity, stateManager, animBoolName)
    {
        this.attackPosition = attackPosition;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        Movement.SetVelocityX(0f);
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
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
