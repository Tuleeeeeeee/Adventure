using UnityEngine;

public class Enemy8 : Entity
{
    public E8_IdleState IdleState { get; private set; }
    public E8_ChargeState ChargeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    public override void Awake()
    {
        base.Awake();

        IdleState = new E8_IdleState(this, stateManager, "idle", idleStateData, this);
        ChargeState = new E8_ChargeState(this, stateManager, "charge", chargeStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
    }
    public override bool CheckIfCanJump() => false;
}

