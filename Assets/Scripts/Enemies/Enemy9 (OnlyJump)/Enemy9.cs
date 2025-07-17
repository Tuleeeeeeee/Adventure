using UnityEngine;

public class Enemy9 : Entity
{
    public E9_IdleState IdleState { get; private set; }
    public E9_JumpState JumpState { get; private set; }
    public E9_InAirState InAirState { get; private set; }
    public E9_Run_UpState Run_UpState { get; private set; }
    public E9_AbilityState AbilityState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_JumpState jumpStateData;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E9_IdleState(this, StateManager, "idle", idleStateData, this);
        JumpState = new E9_JumpState(this, StateManager, "jump", jumpStateData, this);
        InAirState = new E9_InAirState(this, StateManager, "inAir", this);
        Run_UpState = new E9_Run_UpState(this, StateManager, "run-up", this);
        AbilityState = new E9_AbilityState(this, StateManager, "ability", this);
    }
    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(IdleState);
    }
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;

    public override void OnDrawGizmos()
    {
    }
}
