using Tuleeeeee.CoreComponet;
using Tuleeeeee.CoreComponets;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool isJumping;

    private bool wallJumpCoyoteTime;
    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName
    ) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CollisionSenses.Ground;
        isTouchingWall = player.CollisionSenses.WallFront;
        isTouchingWallBack = player.CollisionSenses.WallBack;

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack &&
            (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        CheckJumpMulitiplier();

        if (isGrounded && player.Movement?.CurrentVelocity.y < 0.01f)
        {
            stateManager.ChangeState(player.IdleState);
        }
        if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateManager.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            //player.InputHandler.UsedJumpInput();
            stateManager.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && xInput == player.Movement?.FacingDirection && player.Movement?.CurrentVelocity.y <= 0)
        {
            stateManager.ChangeState(player.WallSlideState);
        }
        else
        {

            player.Movement?.CheckIfShouldFlip(xInput);
            player.Movement?.SetVelocityX(playerData.movementVelocity * xInput);

            player.Animator.SetFloat("yVelocity", player.Movement.CurrentVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(player.Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private void CheckJumpMulitiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.Movement?.SetVelocityY(player.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.Movement?.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;
}