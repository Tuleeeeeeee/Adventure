using UnityEngine;

public class Enemy5 : Entity
{
    public E5_IdleState IdleState { get; private set; }
    public E5_MoveState MoveState { get; private set; }
    public E5_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    public override void Awake()
    {
        base.Awake();

        MoveState = new E5_MoveState(this, stateManager, "move", moveStateData, this);

        IdleState = new E5_IdleState(this, stateManager, "idle", idleStateData, this);

        DeadState = new E5_DeadState(this, stateManager, "dead", deadStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        OnDead(DeadState);
    }
    public override bool CheckPlayerInMinAgroRange() => false;

    public override bool CheckPlayerInMaxAgroRange() => false;

    public override bool CheckPlayerInCloseRangeAction() => false;
}
