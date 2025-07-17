using UnityEngine;
using UnityEngine.Serialization;

public class Enemy17 : Entity
{
    public E17_IdleState IdleState { get; private set; }
    public E17_MoveState MoveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    [SerializeField] private bool isFinal = false;
    [SerializeField] private int spawnCount = 2; // Number of enemies to spawn
    [SerializeField] private GameObject miniRock;

    public override void Awake()
    {
        base.Awake();
        MoveState = new E17_MoveState(this, StateManager, "move", moveStateData, this);
        IdleState = new E17_IdleState(this, StateManager, "idle", idleStateData, this);
    }
    public override void Start()
    {
        StateManager.InitializeEnemy(IdleState);
    }
}