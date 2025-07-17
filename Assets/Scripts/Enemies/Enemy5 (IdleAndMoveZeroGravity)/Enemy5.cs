using UnityEngine;

public class Enemy5 : Entity
{
    public E5_IdleState IdleState { get; private set; }
    public E5_MoveState MoveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E5_MoveState(this, StateManager, "move", moveStateData, this);

        IdleState = new E5_IdleState(this, StateManager, "idle", idleStateData, this);

    }
    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(IdleState);
    }

    public override bool CheckPlayerInMinAgroRange() => false;

    public override bool CheckPlayerInMaxAgroRange() => false;

    public override bool CheckPlayerInCloseRangeAction() => false;
    public override bool CheckIfCanJump() => false;
}
