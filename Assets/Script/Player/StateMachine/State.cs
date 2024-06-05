using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State 
{
    protected Player player;
    protected StateManager stateManager;
    protected PlayerData playerData;

    protected bool isAnimtionFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public State(Player player, StateManager stateManager, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateManager = stateManager;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        player.Animator.SetBool(animBoolName, true);
        Debug.Log(animBoolName);
        isAnimtionFinished = false;
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
    
    public virtual void AnimationFinishedTrigger() => isAnimtionFinished = true;
    
}


