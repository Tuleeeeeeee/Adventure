using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Entity
{
    public E6_IdleState IdleState { get; private set; }
    public E6_MoveState MoveState { get; private set; }
    public E6_JumpState JumpState { get; private set; }
    public E6_AbilityState AbilityState { get; private set; }
    public E6_InAirState InAirState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_JumpState jumpStateData;
    public override void Awake()
    {
        base.Awake();

        MoveState = new E6_MoveState(this, stateManager, "move", moveStateData, this);

        IdleState = new E6_IdleState(this, stateManager, "idle", idleStateData, this);

        JumpState = new E6_JumpState(this, stateManager, "jump", jumpStateData, this);

        AbilityState = new E6_AbilityState(this, stateManager, "ability", this);

        InAirState = new E6_InAirState(this, stateManager, "inAir", this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
    }
    public override bool CheckPlayerInMinAgroRange() => false;

    public override bool CheckPlayerInMaxAgroRange() => false;

    public override bool CheckPlayerInCloseRangeAction() => false;
}
