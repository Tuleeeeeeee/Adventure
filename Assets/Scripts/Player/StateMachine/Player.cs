using Tuleeeeee.CoreComponets;
using Tuleeeeee.Cores;
using Tuleeeeee.Enums;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region COMPONENTS
    public CollisionSenses CollisionSenses =>
        _collisionSenses =
            _collisionSenses != null
                ? _collisionSenses
                : Core.GetCoreComponent(ref _collisionSenses);
    private CollisionSenses _collisionSenses;

    public Health Health =>
        _health = _health != null ? _health : Core.GetCoreComponent(ref _health);
    private Health _health;

    public Movement Movement =>
        _movement = _movement != null ? _movement : Core.GetCoreComponent(ref _movement);
    private Movement _movement;

    public StateManager StateManager { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerAppearState AppearState { get; private set; }
    public PlayerDisappearState DisappearState { get; private set; }
    #endregion

    [SerializeField]
    private PlayerData playerData;

    #region UNITY COMPONENTS
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public BoxCollider2D BoxCollider2D { get; private set; }
    public TrailRenderer TrailRenderer { get; private set; }
    #endregion

    public HealthEvent HealthEvent { get; private set; }

    [SerializeField]
    private GameStateGameEventSO gameStateChangeEvent;

    private bool _controlsEnabled = true;

    #region UNITY METHODS
    void OnEnable()
    {
        HealthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        gameStateChangeEvent.RegisterListener(OnGameStateChanged);
    }

    void OnDisable()
    {
        HealthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
        gameStateChangeEvent.UnregisterListener(OnGameStateChanged);
    }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        BoxCollider2D = GetComponentInChildren<BoxCollider2D>();
        TrailRenderer = GetComponentInChildren<TrailRenderer>();
        StateManager = new StateManager();

        HealthEvent = GetComponentInChildren<HealthEvent>();

        InitializeStates();
    }

    private void Start()
    {
        StateManager.Initialize(IdleState);
    }

    private void Update()
    {
        if (!_controlsEnabled)
            return;
        Core.LogicUpdate();
        StateManager.CurrentPlayerState.LogicUpdate();

        float speed = Movement.CurrentVelocity.magnitude;
        Debug.Log("Player speed: " + speed);
    }

    private void LateUpdate()
    {
        if (!_controlsEnabled) return;

        CollisionSenses.OpTopPlatformMovement();
    }

    private void FixedUpdate()
    {
        if (!_controlsEnabled) return;
        StateManager.CurrentPlayerState.PhysicUpdate();

        CollisionSenses.Platform(BoxCollider2D);
    }
    #endregion

    #region FUNCTIONALITY

    private void SetControlsEnabled(bool enabled)
    {
        InputHandler.enabled = enabled;
        _controlsEnabled = enabled;
        Movement.enabled = enabled;
        Animator.enabled = enabled;
    }

    public void EnableControls()
    {
        SetControlsEnabled(true);
        Movement.Unfreeze();
    }

    public void DisableControls()
    {
        SetControlsEnabled(false);
        Movement.Freeze();
    }

    public void ResetTrail()
    {
        TrailRenderer.Clear();
    }

    #endregion

    #region EVENTS
    private void HealthEvent_OnHealthChanged(
        HealthEvent healthEvent,
        HealthEventArgs healthEventArgs
    )
    {
        if (healthEventArgs.healthAmount <= 0f)
        {
            Debug.Log("Player has died.");
            //destroyedEvent.CallDestroyedEvent(true, 0);
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Playing:
                EnableControls();
                break;
            case GameState.Paused:
            case GameState.LevelCompleted:
            case GameState.GameWon:
                DisableControls();
                break;
        }
    }
    #endregion

    #region INITIALIZATION
    public void Initialize(PlayerDetailsSO playerDetails)
    {
        Health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
    #endregion

    #region ANIM EVENT TRIGGERS
    private void AnimationTrigger() => StateManager.CurrentPlayerState.AnimationTrigger();

    private void AnimationFinishedTrigger() =>
        StateManager.CurrentPlayerState.AnimationFinishedTrigger();
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
        AppearState = new PlayerAppearState(this, StateManager, playerData, "appear");
        DisappearState = new PlayerDisappearState(this, StateManager, playerData, "disappear");
    }

    #endregion
}
