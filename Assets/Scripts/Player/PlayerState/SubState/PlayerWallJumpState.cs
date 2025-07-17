using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UsedJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();

        player.Movement?.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.Movement?.CheckIfShouldFlip(-wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Animator.SetFloat("yVelocity", player.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", Mathf.Abs((float)player.Movement.CurrentVelocity.x));

    }
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.Movement.FacingDirection;
        }
    }
}
