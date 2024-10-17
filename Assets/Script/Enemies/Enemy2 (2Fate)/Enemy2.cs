using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState IdleState { get; private set; }
    public E2_MoveState MoveState { get; private set; }
    public E2_AngryIdleState AngryIdleState { get; private set; }
    public E2_AngryMoveState AngryMoveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;    

    [SerializeField]
    private D_IdleState angryIdleStateData;
    [SerializeField]
    private D_MoveState angryMoveStateData;

    public override void Awake()
    {
        base.Awake();
        IdleState = new E2_IdleState(this, stateManager, "idle", idleStateData, this);
        MoveState = new E2_MoveState(this, stateManager, "move", moveStateData, this);
        AngryIdleState = new E2_AngryIdleState(this, stateManager, "angry", angryIdleStateData, this);
        AngryMoveState = new E2_AngryMoveState(this, stateManager, "angry", angryMoveStateData, this);
    }

    public override void Start()
    {
        stateManager.InitializeEnemy(MoveState);
    }
    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;
    public override void OnDrawGizmos()
    {
    }
}
