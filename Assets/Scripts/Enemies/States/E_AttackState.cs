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

        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        IsAnimationFinished = false;
      //  Movement.SetVelocityX(0f);
    }

}
