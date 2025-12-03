public class E_JumpState : E_AbilityState
{
    protected D_JumpState stateData;

    protected int amountOfJumpsLeft;
    public E_JumpState(Entity entity, StateManager stateManager, string animBoolName, D_JumpState stateData) : base(entity, stateManager, animBoolName)
    {
        this.stateData = stateData;
        amountOfJumpsLeft = stateData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityY(stateData.jumpVelocity);

        //Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);

        isAbilityDone = true;
        amountOfJumpsLeft--;
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
    
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = stateData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    public int AmountOfJumpLeft() => amountOfJumpsLeft;
}
