using UnityEngine;

public class Enemy4 : Entity
{
    public E4_IdleState IdleState { get; private set; }
    public E4_MoveState MoveState { get; private set; }
    public E4_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E4_LookForPlayerState LookForPlayerState { get; private set; }
    public E4_LongRangeAttack LongRangeAttack { get; private set; }
    public E4_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;

    [SerializeField]
    private D_MoveState moveStateData;

    [SerializeField]
    private D_PlayerDetected playerDetectedData;

    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;

    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;

    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E4_MoveState(this, stateManager, "move", moveStateData, this);

        IdleState = new E4_IdleState(this, stateManager, "idle", idleStateData, this);

        PlayerDetectedState = new E4_PlayerDetectedState(this, stateManager, "detected", playerDetectedData, this);

        LookForPlayerState = new E4_LookForPlayerState(this, stateManager, "look", lookForPlayerStateData, this);

        LongRangeAttack = new E4_LongRangeAttack(this, stateManager, "attack", rangedAttackPosition, rangedAttackStateData, this);

    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
    }
  
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override bool CheckIfCanJump() => false;
}
