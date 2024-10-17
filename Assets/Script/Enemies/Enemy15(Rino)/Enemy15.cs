using UnityEngine;

public class Enemy15 : Entity
{
    public E15_IdleState IdleState { get; private set; }
    public E15_ChargeState ChargeState { get; private set; }
    public E15_LookForPlayerState LookForPlayerState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;

    [SerializeField]
    private D_ChargeState chargeStateData;

    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E15_IdleState(this, stateManager, "idle", idleStateData, this);

        ChargeState = new E15_ChargeState(this, stateManager, "charge", chargeStateData, this);

        LookForPlayerState = new E15_LookForPlayerState(this, stateManager, "look", lookForPlayerStateData, this);

    }
    public override void Start()
    {
        stateManager.InitializeEnemy(IdleState);       
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override bool CheckIfCanJump() => false;
}
