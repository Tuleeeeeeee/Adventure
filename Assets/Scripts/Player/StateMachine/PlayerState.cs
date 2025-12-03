using Tuleeeeee.Cores;
using UnityEngine;

public class PlayerState
{
    protected Core Core;
    protected Player Player;
    protected StateManager StateManager;
    protected PlayerData PlayerData;
    protected bool IsAnimationFinished;
    protected bool IsExitingState;
    protected float StartTime;
    private string _animBoolName;

    public PlayerState(Player player, StateManager stateManager, PlayerData playerData, string animBoolName)
    {
        this.Player = player;
        this.StateManager = stateManager;
        this.PlayerData = playerData;
        this._animBoolName = animBoolName;
        Core = player.Core;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public virtual void Enter()
    {
        DoCheck();
        StartTime = Time.time;
        Player.Animator.SetBool(_animBoolName, true);
        Debug.Log($"State: {_animBoolName}");
        IsAnimationFinished = false;
        IsExitingState = false;
    }
    public virtual void Exit()
    {
        Player.Animator.SetBool(_animBoolName, false);
        IsExitingState = true;
    }
    public virtual void LogicUpdate()
    {

    }
    // ReSharper disable Unity.PerformanceAnalysis
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

    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;

}


