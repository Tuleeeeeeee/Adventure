using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.CanCreateDuskJump();
        player.JumpAudio();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        return false;
    }
    public bool CanDoubleJump()
    {
        if (amountOfJumpsLeft == 1)
        {
            return true;
        }
        return false;
    }
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    public int AmountOfJumpLeft() => amountOfJumpsLeft;
}
