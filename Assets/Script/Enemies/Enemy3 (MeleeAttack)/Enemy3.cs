using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState IdleState { get; private set; }
    public E3_MoveState MoveState { get; private set; }
    public E3_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E3_ChargeState ChargeState { get; private set; }
    public E3_LookForPlayerState LookForPlayerState { get; private set; }
    public E3_MeleeAtackState MeleeAtackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;

    [SerializeField]
    private D_MoveState moveStateData;

    [SerializeField]
    private D_PlayerDetected playerDetectedData;

    [SerializeField]
    private D_ChargeState chargeStateData;

    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;

    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private bool isFilp;
    public override void Awake()
    {
        base.Awake();

        MoveState = new E3_MoveState(this, stateManager, "move", moveStateData, this);

        IdleState = new E3_IdleState(this, stateManager, "idle", idleStateData, this);

        PlayerDetectedState = new E3_PlayerDetectedState(this, stateManager, "detected", playerDetectedData, this);

        ChargeState = new E3_ChargeState(this, stateManager, "charge", chargeStateData, this);

        LookForPlayerState = new E3_LookForPlayerState(this, stateManager, "look", lookForPlayerStateData, this);

        MeleeAtackState = new E3_MeleeAtackState(this, stateManager, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
        if (isFilp) Movement.Flip();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override bool CheckIfCanJump() => false;
}
