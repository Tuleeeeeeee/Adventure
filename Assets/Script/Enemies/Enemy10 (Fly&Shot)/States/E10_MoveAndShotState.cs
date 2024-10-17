using UnityEngine;

public class E10_MoveAndShotState : E_MoveAndShotState
{

    private float attackDuration = 0.35f;  // Duration of the attack animation
    private float attackCooldown = 2f;      // Cooldown time before the next attack

    private float attackTimer = 0f;                          // Timer for attack duration
    private float cooldownTimer = 0f;                        // Timer for cooldown between attacks
    private bool isAttacking = false;                        // Flag to track if the enemy is attacking

    private bool playerInRange;
    private Enemy10 enemy;

    public E10_MoveAndShotState(Entity entity, StateManager stateManager, string animBoolName, D_MoveAndAttack stateData, Transform attackPosition, Enemy10 enemy) : base(entity, stateManager, animBoolName, stateData, attackPosition)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        playerInRange = enemy.CheckPlayerInMaxAgroRange(); // Check if the player is in range
    }

    public override void Enter()
    {
        base.Enter();
        stateData.angle = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (playerInRange && !isAttacking && cooldownTimer <= 0f)
        {
            // Player is in range, not currently attacking, and cooldown is over
            StartAttack();
        }
        else if (isAttacking)
        {
            // The enemy is attacking, decrease the attack timer
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                // Attack duration is over, go back to idle
                EndAttack();
            }
        }
        else if (cooldownTimer > 0f)
        {
            // Decrease cooldown timer if it is active
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void StartAttack()
    {
        isAttacking = true;               // Start the attack
        attackTimer = attackDuration;     // Reset the attack timer
        entity.Animator.SetFloat("action", 1f);  // Set to attack animation
    }
    private void EndAttack()
    {
        isAttacking = false;              // End the attack
        entity.Animator.SetFloat("action", 0f);  // Set to idle animation
        cooldownTimer = attackCooldown;   // Start cooldown timer
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
