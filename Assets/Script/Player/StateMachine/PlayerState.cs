using UnityEngine;

public class PlayerState
{
    protected Core core;
    protected Player player;
    protected StateManager stateManager;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateManager = stateManager;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.Core;
    }
    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        player.Animator.SetBool(animBoolName, true);
        Debug.Log($"Player: {animBoolName}");
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        player.Animator.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        DoCheck();
    }
    public virtual void DoCheck()
    {

    }
    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;

}


