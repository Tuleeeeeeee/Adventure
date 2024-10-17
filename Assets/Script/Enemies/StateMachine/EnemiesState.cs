using UnityEngine;

public class EnemiesState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected Movement movement;
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected CollisionSenses collisionSenses;

    protected Entity entity;
    protected StateManager stateManager;
    protected Core core;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    public float startTime { get; protected set; }

    private string animBoolName;
    public EnemiesState(Entity entity, StateManager stateManager, string animBoolName)
    {
        this.entity = entity;
        this.stateManager = stateManager;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        entity.Animator.SetBool(animBoolName, true);
        Debug.Log($"{entity.name}: {animBoolName}");
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
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
}
