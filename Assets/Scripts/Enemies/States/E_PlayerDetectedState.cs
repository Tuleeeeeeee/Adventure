using UnityEngine;

public class E_PlayerDetectedState : EnemiesState
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;

    protected bool isDetectingLedge;

    public E_PlayerDetectedState(Entity entity, StateManager stateManager, string animBoolName,
        D_PlayerDetected stateData) : base(entity, stateManager, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();

        isDetectingLedge = CollisionSenses.LedgeVertical;
        performCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;

        Movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }
}