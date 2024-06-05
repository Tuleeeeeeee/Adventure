using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : State
{
    protected int xInput;
    private bool jumpInput;
    private bool isGrounded;
    
    public PlayerGroundState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        if (jumpInput && player.JumpState.CanJump())
        {
           
            stateManager.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateManager.ChangeState(player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
