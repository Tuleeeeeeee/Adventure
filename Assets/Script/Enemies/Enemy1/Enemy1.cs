using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    /*public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }*/

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;    
    [SerializeField]
 /*   private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;*/

    public override void Start()
    {
        base.Start();

        MoveState = new E1_MoveState(this, stateManager, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateManager, "idle", idleStateData, this);
       /* PlayerDetectedState = new E1_PlayerDetectedState(this, stateManager, "detected", playerDetectedData, this);
        ChargeState = new E1_ChargeState(this, stateManager, "charge", chargeStateData, this);
        LookForPlayerState = new E1_LookForPlayerState(this, stateManager, "charge", lookForPlayerData, this);*/


        stateManager.InitializeEnemy(MoveState);

    }

}
