using UnityEngine;

public class Enemy14 : Entity
{
    public E14_IdleState IdleStates { get; private set; }
    public E14_MoveState MoveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public GameObject Slime { get => slime; private set => slime = value; }
    [SerializeField]
    private GameObject slime;
    public override void Awake()
    {
        base.Awake();

        IdleStates = new E14_IdleState(this, stateManager, "idle", idleStateData, this);
        MoveState = new E14_MoveState(this, stateManager, "idle", moveStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleStates);
    }
    public override void Update()
    {
        base.Update();
    }
    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;
}
