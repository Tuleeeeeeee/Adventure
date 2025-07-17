using UnityEngine;

public class E_MeleeAttackState : E_AttackState
{
    protected D_MeleeAttack stateData;

    public E_MeleeAttackState(Entity entity, StateManager stateManager, string animBoolName, Transform attackPosition,
        D_MeleeAttack stateData) : base(entity, stateManager, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Collider2D[] detectedObjects =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.DealDamage(stateData.attackDamage);
            }
        }
    }
}