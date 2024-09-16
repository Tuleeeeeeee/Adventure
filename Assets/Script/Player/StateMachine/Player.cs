using UnityEngine;
public class Player : MonoBehaviour
{
    #region Components
    private CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses); }

    private CollisionSenses collisionSenses;
    public StateManager StateManager { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    #endregion

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private ParticleSystem dustMoveEffect;
    [SerializeField]
    private ParticleSystem dustJumpEffect;
    [SerializeField]
    private ParticleSystem dustWallJumpEffect;
    [SerializeField]
    private AudioSource jumpAudio;

    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    private Rigidbody2D platformRBody;
    private Vector2 lastPlatformPosition;
    private bool isOnPlatform;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateManager = new StateManager();
        IdleState = new PlayerIdleState(this, StateManager, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateManager, playerData, "move");
        JumpState = new PlayerJumpState(this, StateManager, playerData, "in_air");
        DoubleJumpState = new PlayerDoubleJumpState(this, StateManager, playerData, "double_jump");
        InAirState = new PlayerInAirState(this, StateManager, playerData, "in_air");
        LandState = new PlayerLandState(this, StateManager, playerData, null);
        WallSlideState = new PlayerWallSlideState(this, StateManager, playerData, "wall_slide");
        WallJumpState = new PlayerWallJumpState(this, StateManager, playerData, "wall_jump");

    }
    private void Start()
    {
        Animator.SetTrigger("appear");
        StateManager.Initialize(IdleState);
    }
    private void Update()
    {
        Core.LogicUpdate();
        StateManager.CurrentPlayerState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateManager.CurrentPlayerState.PhysicUpdate();
        attackEnemy();
        CheckIfOnPlatform();
        if (isOnPlatform)
        {
            Vector2 deltaPosition = platformRBody.position - lastPlatformPosition;
            RB.position += deltaPosition;
            lastPlatformPosition = platformRBody.position;
        }

    }
    private void AnimationTrigger() => StateManager.CurrentPlayerState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateManager.CurrentPlayerState.AnimationFinishedTrigger();
    private void attackEnemy()
    {
        Collider2D enemyHit = Physics2D.OverlapBox(CollisionSenses.GroundCheck.position, CollisionSenses.GroundCheckSize, 0f, playerData.whatIsEnemy);
        if (enemyHit)
        {
            IDamageable damageable = enemyHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(100);
                RB.velocity = new Vector2(RB.velocity.x, 20f);
            }
        }
    }
    public void CheckIfOnPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(RB.transform.position, Vector2.down, playerData.platformCheckDistance, playerData.whatIsPlatfrom);
        if (hit.collider != null)
        {
            platformRBody = hit.collider.GetComponent<Rigidbody2D>();
            if (platformRBody != null)
            {
                if (!isOnPlatform)
                {
                    lastPlatformPosition = platformRBody.position;
                }
            }
            isOnPlatform = true;
        }
        else
        {
            isOnPlatform = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = isOnPlatform ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * playerData.platformCheckDistance));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ICollectible collectible = col.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.OnCollected();
        }
    }
}
