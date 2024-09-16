using UnityEngine;

public class E_MeleeAttackState : E_AttackState
{
    protected D_MeleeAttack stateData;

    protected AttackDetails attackDetails;
    public E_MeleeAttackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateManager, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(stateData.attackDamage);
            }
        }
    }
}
