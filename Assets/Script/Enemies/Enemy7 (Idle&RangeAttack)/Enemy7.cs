using UnityEngine;

public class Enemy7 : Entity
{
    public E7_IdleState IdleState { get; private set; }
    public E7_RangeAttackState RangeAttackState { get; private set; }
    public E7_DetectedState DetectedState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected detectedStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;


    [SerializeField]
    private Transform rangedAttackPosition;
    [SerializeField]
    private bool filp;
    public override void Awake()
    {
        base.Awake();

        IdleState = new E7_IdleState(this, stateManager, "idle", idleStateData, this);
        RangeAttackState = new E7_RangeAttackState(this, stateManager, "attack", rangedAttackPosition, rangedAttackStateData, this);
        DetectedState = new E7_DetectedState(this, stateManager, "detected", detectedStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(IdleState);
        if(filp) Movement.Flip();
    }
    public override bool CheckIfCanJump() => false;
}
