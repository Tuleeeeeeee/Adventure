using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState IdleState { get; private set; }
    public E3_MoveState MoveState { get; private set; }
    public E2_ChargeState ChargeState { get; private set; }
    public E2_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E2_LookForPlayerState LookForPlayerState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;

    public override void Start()
    {
        base.Start();

        MoveState = new E3_MoveState(this, stateManager, "move", moveStateData, this);
        IdleState = new E3_IdleState(this, stateManager, "idle", idleStateData, this);
      /*  ChargeState = new E2_ChargeState(this, stateManager, "charge", chargeStateData, this);
        LookForPlayerState = new E2_LookForPlayerState(this, stateManager, "look", lookForPlayerData, this);
        PlayerDetectedState = new E2_PlayerDetectedState(this, stateManager, "detected", playerDetectedData, this);
*/

        stateManager.InitializeEnemy(MoveState);

    }
}
