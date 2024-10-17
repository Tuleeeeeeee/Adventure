using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E1_MoveState(this, stateManager, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateManager, "idle", idleStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(MoveState);
    }
    public override bool CheckPlayerInMinAgroRange() => false;

    public override bool CheckPlayerInMaxAgroRange() => false;

    public override bool CheckPlayerInCloseRangeAction() => false;

    public override bool CheckIfCanJump() => false;

}
