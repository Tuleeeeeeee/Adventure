
public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) : base(
        player, stateManager, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UsedJumpInput();
        Player.Movement?.SetVelocityY(PlayerData.jumpHeight);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        Player.InAirState.SetIsJumping();
    }
    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = PlayerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}