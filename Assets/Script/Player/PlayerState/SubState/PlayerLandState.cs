using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateManager.ChangeState(player.MoveState);
            }
            else if (player.CurrentVelocity.y < 0.01f)
            {
                stateManager.ChangeState(player.IdleState);
            }
        }
    }
}
