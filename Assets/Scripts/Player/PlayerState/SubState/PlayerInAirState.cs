using Tuleeeeee.CoreComponet;
using Tuleeeeee.CoreComponets;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingWallBack;
    private bool _oldIsTouchingWall;
    private bool _oldIsTouchingWallBack;
    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _jumpEndedEarly;
    private bool _coyoteTime;
    private bool _isJumping;

    private bool _wallJumpCoyoteTime;
    private float _startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName
    ) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;

        _isGrounded = Player.CollisionSenses.Ground;
        _isTouchingWall = Player.CollisionSenses.WallFront;
        _isTouchingWallBack = Player.CollisionSenses.WallBack;

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack &&
            (_oldIsTouchingWall || _oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        _xInput = Player.InputHandler.NormInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _jumpEndedEarly = Player.InputHandler.JumpInputStop && Player.Movement.CurrentVelocity.y > 0f;

        CheckJumpMultiplier();

        if (_isGrounded && Player.Movement?.CurrentVelocity.y < 0.01f)
        {
            StateManager.ChangeState(Player.IdleState);
        }
        if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            StateManager.ChangeState(Player.WallJumpState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            //player.InputHandler.UsedJumpInput();
            StateManager.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && _xInput == Player.Movement?.FacingDirection && Player.Movement?.CurrentVelocity.y <= 0)
        {
            StateManager.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.Movement?.CheckIfShouldFlip(_xInput);
            Player.Movement?.SetVelocityX(PlayerData.movementVelocity * _xInput, PlayerData.airDeceleration);
            

            Player.Animator.SetFloat("yVelocity", Player.Movement.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Player.Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Player.Movement?.ApplyGravity(PlayerData.fallAcceleration, PlayerData.maxFallSpeed, 
            PlayerData.groundingForce, PlayerData.jumpEndEarlyGravityModifier, _isGrounded, _jumpEndedEarly);
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Player.Movement?.SetVelocityY(Player.Movement.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Player.Movement?.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + PlayerData.coyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > StartTime + PlayerData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;
}