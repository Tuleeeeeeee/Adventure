using UnityEngine;

public class Enemy14 : Entity
{
    public E14_IdleState IdleStates { get; private set; }
    public E14_MoveState MoveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public Transform spawnSlime { get; private set; }

    public override void Awake()
    {
        base.Awake();
        spawnSlime = transform.Find("Spawn");
        IdleStates = new E14_IdleState(this, StateManager, "idle", idleStateData, this);
        MoveState = new E14_MoveState(this, StateManager, "idle", moveStateData, this);
    }

    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(IdleStates);
    }

    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;
}