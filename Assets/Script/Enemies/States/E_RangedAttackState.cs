using UnityEngine;

public class E_RangedAttackState : E_AttackState
{
    protected D_RangedAttackState stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;

    public E_RangedAttackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition, D_RangedAttackState stateData) : base(entity, stateManager, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        //  projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = ObjectPool.DequeueObject<Projectile>("bullet");
        projectileScript.gameObject.SetActive(true);
        projectileScript.FireProjectile(stateData.projectileSpeed * Movement.FacingDirection, stateData.projectileTravelDistance, stateData.projectileDamage);
        projectileScript.Initialize();
        projectileScript.transform.position = attackPosition.position;
        projectileScript.transform.rotation = attackPosition.rotation;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
