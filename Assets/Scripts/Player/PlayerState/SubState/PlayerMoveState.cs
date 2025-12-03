using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{

    public PlayerMoveState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(
        player, stateManager, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.Movement?.CheckIfShouldFlip(XInput);

        if (XInput == 0 && !IsExitingState)
        {
            StateManager.ChangeState(Player.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Player.Movement.SetVelocityX(PlayerData.movementVelocity * XInput, PlayerData.acceleration);
    }
}