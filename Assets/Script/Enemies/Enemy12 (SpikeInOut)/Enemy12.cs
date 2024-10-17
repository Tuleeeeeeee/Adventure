using UnityEngine;

public class Enemy12 : Entity
{
    public E12_SpikeOutIdleState SpikeOutIdleState { get; private set; }
    public E12_SpikeInIdleState SpikeInIdleState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    public override void Awake()
    {
        base.Awake();
        SpikeOutIdleState = new E12_SpikeOutIdleState(this, stateManager, "SpikeOut", idleStateData, this);
        SpikeInIdleState = new E12_SpikeInIdleState(this, stateManager, "SpikeIn", idleStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(SpikeOutIdleState);
    }
    public override void UpdateDamageableArea(Vector2 newSize, Vector3 newPosition)
    {
        base.UpdateDamageableArea(newSize, newPosition);
    }
    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;

    
}
