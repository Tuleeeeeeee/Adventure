using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerDoubleJumpState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public bool CanDoubleJump()
    {
        if (amountOfJumpsLeft == 1)
        {
            return true;
        }
        return false;
    }
}

