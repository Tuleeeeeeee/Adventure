using Tuleeeeee.Cores;
using Tuleeeeee.CoreComponets;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    #region COMPONENTS
    public CollisionSenses CollisionSenses => collisionSenses = collisionSenses != null ? collisionSenses : Core.GetCoreComponent(ref collisionSenses);
    private CollisionSenses collisionSenses;

    public Health Health => health = health != null ? health : Core.GetCoreComponent(ref health);
    private Health health;

    public Movement Movement => movement = movement != null ? movement : Core.GetCoreComponent(ref movement);
    private Movement movement;

    public StateManager StateManager { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    #endregion

    [SerializeField] private PlayerData playerData;

    #region UNITY COMPONENTS
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public BoxCollider2D BoxCollider2D { get; private set; }
    #endregion

    public HealthEvent HealthEvent { get; private set; }

    // Platform interaction
    private Rigidbody2D currentPlatformRBody;
    private Vector2 lastPlatformPosition;
    private bool isOnPlatform;

    private Vector3 originalScale;

    #region UNITY METHODS
    void OnEnable()
    {
        HealthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }
    void OnDisable()
    {
        HealthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        BoxCollider2D = GetComponentInChildren<BoxCollider2D>();
        StateManager = new StateManager();

        HealthEvent = GetComponentInChildren<HealthEvent>();

        InitializeStates();
    }

    private void Start()
    {
        Health.SetStartingHealth(100);
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

        CheckIfOnPlatform();

        if (isOnPlatform && currentPlatformRBody != null)
        {
            Vector2 platformMovement = currentPlatformRBody.position - lastPlatformPosition;
            Movement.ApplyPositionOffset(platformMovement);
            lastPlatformPosition = currentPlatformRBody.position;
        }
    }
    #endregion

    #region EVENTS
    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        if (healthEventArgs.healthAmount <= 0f)
        {
            Debug.Log("Player has died.");
            //destroyedEvent.CallDestroyedEvent(true, 0);
        }
    }
    #endregion

    #region INITIALIZATION
    public void Initialize(PlayerDetailsSO playerDetails)
    {

    }
    #endregion

    #region PLATFORM CHECK
    private void CheckIfOnPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            BoxCollider2D.bounds.center,
            BoxCollider2D.bounds.size,
            0f,
            Vector2.down,
            playerData.platformCheckDistance,
            playerData.whatIsPlatfrom
        );

        if (hit.collider?.attachedRigidbody != null)
        {
            currentPlatformRBody = hit.collider.attachedRigidbody;

            if (!isOnPlatform)
            {
                lastPlatformPosition = currentPlatformRBody.position;
            }

            isOnPlatform = true;
        }
        else
        {
            isOnPlatform = false;
            currentPlatformRBody = null;
        }
    }
    #endregion

    #region ANIM EVENT TRIGGERS
    private void AnimationTrigger() => StateManager.CurrentPlayerState.AnimationTrigger();
    private void AnimationFinishedTrigger() => StateManager.CurrentPlayerState.AnimationFinishedTrigger();
    #endregion

    #region STATE INITIALIZATION
    private void InitializeStates()
    {
        IdleState = new PlayerIdleState(this, StateManager, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateManager, playerData, "move");
        JumpState = new PlayerJumpState(this, StateManager, playerData, "in_air");
        InAirState = new PlayerInAirState(this, StateManager, playerData, "in_air");
        WallSlideState = new PlayerWallSlideState(this, StateManager, playerData, "wall_slide");
        WallJumpState = new PlayerWallJumpState(this, StateManager, playerData, "wall_jump");
    }

    #endregion
}
