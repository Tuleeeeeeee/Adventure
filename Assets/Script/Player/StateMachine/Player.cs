using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateManager StateManager { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    public AudioManager audioManager { get; private set; }
    [SerializeField]
    private ParticleSystem dustMoveEffect;
    [SerializeField]
    private ParticleSystem dustJumpEffect;
    [SerializeField]
    private ParticleSystem dustWallJumpEffect;
    public Animator Animator { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;
    public int FacingDirection { get; private set; }

    private void Awake()
    {
       
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
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        FacingDirection = 1;
        Animator.SetTrigger("appear");
        StateManager.Initialize(IdleState);
      

    }
    private void Update()
    {
        CurrentVelocity = RB.velocity;
        CheckIfGrounded();
        StateManager.CurrentState.LogicUpdate();
        Debug.DrawRay(wallCheck.position, Vector2.right * FacingDirection, Color.green);

    }
    private void FixedUpdate()
    {
        StateManager.CurrentState.PhysicUpdate();
    }


    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    private void AnimationTrigger() => StateManager.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateManager.CurrentState.AnimationFinishedTrigger();


    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, playerData.groundCheckRadius);

    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    public void CanCreateDusk(int xInput)
    {
            DuskMoveEffect();
          //  Debug.Log("dust");
    }
    public void JumpAudio()
    {
       
    }
    public void CanCreateDuskJump()
    {
        DuskJumpEffect();
        //Debug.Log("dust_jump");
    }
    public void CanCreateDuskWallJump()
    {
        DuskWallJumpEffect();
        //Debug.Log("dust_jump");
    }

    private void Flip()
    {
        /*FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);*/
        FacingDirection *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void DuskMoveEffect()
    {
        dustMoveEffect.Play();
    }
    private void DuskJumpEffect()
    {
        dustJumpEffect.Play();
    }
    private void DuskWallJumpEffect()
    {
        dustWallJumpEffect.Play();
    }

}
