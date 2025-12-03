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
        Player.InputHandler.UsedJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();

        Player.Movement?.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, wallJumpDirection);
        Player.Movement?.CheckIfShouldFlip(-wallJumpDirection);
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + PlayerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Player.Animator.SetFloat("yVelocity", Player.Movement.CurrentVelocity.y);
        Player.Animator.SetFloat("xVelocity", Mathf.Abs((float)Player.Movement.CurrentVelocity.x));

    }
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Player.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = Player.Movement.FacingDirection;
        }
    }
}
