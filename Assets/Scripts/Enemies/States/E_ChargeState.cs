using UnityEngine;

public class E_ChargeState : EnemiesState
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool isDectectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;

    protected bool performCloseRangeAction;

    public E_ChargeState(Entity entity, StateManager stateManager, string animBoolName, D_ChargeState stateData) : base(
        entity, stateManager, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();

        isDectectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;

        performCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }
}