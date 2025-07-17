using System.Collections.Generic;
using Tuleeeeee.Pathfinding;
using UnityEngine;

public class Enemy8 : Entity
{
    public E8_IdleState IdleState { get; private set; }
    public E8_MoveState MoveState { get; private set; }
    public E8_LookForPlayer LookForPlayer { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerData;

    public List<Vector3> pathVectorList;
    public int currentPathIndex;

    public override void Awake()
    {
        base.Awake();

        IdleState = new E8_IdleState(this, StateManager, "idle", idleStateData, this);
        MoveState = new E8_MoveState(this, StateManager, "move", moveStateData, this);
        LookForPlayer = new E8_LookForPlayer(this, StateManager, "look", lookForPlayerData, this);
    }

    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(IdleState);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetTargetPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerCheck.position, transform.right * Movement.FacingDirection,
            entityData.maxAgroDistance,
            entityData.whatIsPlayer | entityData.whatIsGround);
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & entityData.whatIsPlayer) != 0)
            {
                return hit.transform.position;
            }
        }

        return Vector3.zero;
    }
}