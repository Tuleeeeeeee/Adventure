using UnityEngine;

public class Enemy7 : Entity
{
    public E7_IdleState IdleState { get; private set; }
    public E7_RangeAttackState RangeAttackState { get; private set; }
    public E7_DetectedState DetectedState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected detectedStateData;
    [SerializeField] private D_RangedAttackState rangedAttackStateData;


    [SerializeField] private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E7_IdleState(this, StateManager, "idle", idleStateData, this);
        RangeAttackState = new E7_RangeAttackState(this, StateManager, "attack", rangedAttackPosition,
            rangedAttackStateData, this);
        DetectedState = new E7_DetectedState(this, StateManager, "detected", detectedStateData, this);
    }

    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(IdleState);
    }

    public override bool CheckIfCanJump() => false;
}