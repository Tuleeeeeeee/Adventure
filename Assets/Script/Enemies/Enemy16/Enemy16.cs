using UnityEngine;
public class Enemy16 : Entity
{
    public E_BounceState BounceState { get; private set; }

    public float speed = 5f; // Speed of the enemy
    public float detectionRadius = 0.5f; // Radius to check for obstacles
    public Vector2 direction;

    public override void Awake()
    {
        base.Awake();
        BounceState = new E_BounceState(this, stateManager, "idle", this);
    }
    public override void Start()
    {
        stateManager.InitializeEnemy(BounceState);
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector2)transform.position + direction * detectionRadius, detectionRadius);
    }
}
