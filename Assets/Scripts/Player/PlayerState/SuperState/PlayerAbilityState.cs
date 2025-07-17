using Tuleeeeee.CoreComponets;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;

    public PlayerAbilityState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName) :
        base(player, stateManager, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded =  player.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone) 
        {
            if (isGrounded &&  player.Movement?.CurrentVelocity.y < 0.01f)
            {
                stateManager.ChangeState(player.IdleState);
            }
            else
            {
                stateManager.ChangeState(player.InAirState);
            }
        }
    }
}