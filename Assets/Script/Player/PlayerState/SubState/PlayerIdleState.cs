using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(player, stateManager, playerData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput != 0 && !isExitingState)
        {
            stateManager.ChangeState(player.MoveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
