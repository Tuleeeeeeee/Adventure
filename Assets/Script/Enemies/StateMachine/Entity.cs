using UnityEngine;

public class Entity : MonoBehaviour
{
    public StateManager stateManager;

    public D_Entity entityData;
    public int FacingDirection { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGO { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    private Vector2 velocityWorkspace;

    public virtual void Awake()
    {
        AliveGO = transform.Find("Alive").gameObject;
    }
    public virtual void Start()
    {
        FacingDirection = -1;
        RB = GetComponentInChildren<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        stateManager = new StateManager();
    }

    public virtual void Update()
    {
        stateManager.CurrentEnemyState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateManager.CurrentEnemyState.PhysicUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(FacingDirection * velocity, RB.velocity.y);
        RB.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right * FacingDirection, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right * FacingDirection, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual void Flip()
    {
        /* FacingDirection *= -1;
         AliveGO.transform.Rotate(0f, 180f, 0f);*/

        FacingDirection *= -1;
        Vector3 localScale = AliveGO.transform.localScale;
        localScale.x *= -1f;
        AliveGO.transform.localScale = localScale;
    }

    public virtual void OnDrawGizmos()
    {
        // Ledge Check
        Gizmos.color = CheckLedge() ? Color.green : Color.red;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        // Wall Check
        Gizmos.color = CheckWall() ? Color.green : Color.red;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * entityData.wallCheckDistance));

        //
        Gizmos.color = CheckPlayerInMinAgroRange() ? Color.green : Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (transform.right * FacingDirection * entityData.minAgroDistance));

        //
        Gizmos.color = CheckPlayerInMaxAgroRange() ? Color.gray : Color.cyan;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (transform.right * FacingDirection * entityData.maxAgroDistance));

    }
}
