using Tuleeeeee.Cores;
using Tuleeeeee.CoreComponets;
using UnityEngine;

public class EnemiesState
{
    protected Movement Movement { get => _movement ?? Core.GetCoreComponent(ref _movement); }
    private Movement _movement;
    protected CollisionSenses CollisionSenses { get => _collisionSenses ?? Core.GetCoreComponent(ref _collisionSenses); }
    private CollisionSenses _collisionSenses;

    protected Entity Entity;
    protected StateManager StateManager;
    protected Core Core;

    protected bool IsAnimationFinished;
    protected bool IsExitingState;
    public float StartTime { get; protected set; }

    private string _animBoolName;
    public EnemiesState(Entity entity, StateManager stateManager, string animBoolName)
    {
        this.Entity = entity;
        this.StateManager = stateManager;
        this._animBoolName = animBoolName;
        Core = entity.Core;
    }
    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.time;
        Entity.Animator.SetBool(_animBoolName, true);
        Debug.Log($"{Entity.name}: {_animBoolName}");
        IsAnimationFinished = false;
        IsExitingState = false;
    }
    public virtual void Exit()
    {
        Entity.Animator.SetBool(_animBoolName, false);
        IsExitingState = true;
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
    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
}
