using UnityEngine;
public class Enemy16 : Entity
{
    public E_BounceState BounceState { get; private set; }
    [SerializeField]
    private float speed = 5f; // Speed of the enemy
    public float Speed { get { return speed; } private set { speed = value; } }

    [SerializeField]
    private float detectionRadius = 0.5f; // Radius to check for obstacles
    public float DetectionRadius { get { return detectionRadius; } private set { detectionRadius = value; } }

    public Vector2 direction;

    private Vector2 idle2DamableArea = new Vector2(1f, 0.3f);
    private Vector2 idle1DamableArea = new Vector2(1.4f, 1.4f);

    private Vector3 idle2DamableAreaPos = new Vector3(0, -0.4f, 0);
    private Vector3 idle1DamableAreaPos = Vector3.zero;

    public Vector2 Idle2DamableArea
    {
        get { return idle2DamableArea; }
        private set { idle2DamableArea = value; }
    }

    public Vector2 Idle1DamableArea
    {
        get { return idle1DamableArea; }
        private set { idle1DamableArea = value; }
    }

    public Vector3 Idle2DamableAreaPos
    {
        get { return idle2DamableAreaPos; }
        private set { idle2DamableAreaPos = value; }
    }

    public Vector3 Idle1DamableAreaPos
    {
        get { return idle1DamableAreaPos; }
        private set { idle1DamableAreaPos = value; }
    }

    public override void Awake()
    {
        base.Awake();
        entityData = Instantiate(entityData);
        BounceState = new E_BounceState(this, StateManager, "idle", this);

    }
    public override void Start()
    {
        StateManager.InitializeEnemy(BounceState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector2)transform.position + direction * detectionRadius, detectionRadius);
    }
}
