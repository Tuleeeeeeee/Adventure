using UnityEngine;

public class EnemiesState 
{
    protected Entity entity;
    protected StateManager stateManager;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;
    public EnemiesState (Entity entity, StateManager stateManager, string animBoolName)
    {
        this.entity = entity;
        this.stateManager = stateManager;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        entity.Animator.SetBool(animBoolName, true);
        Debug.Log($"Enemies: {animBoolName}");
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        entity.Animator.SetBool(animBoolName, false);
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
    public virtual void DebugLog(string nameEnemy)
    {
        Debug.Log($"{nameEnemy}: {animBoolName}");
    }
}
