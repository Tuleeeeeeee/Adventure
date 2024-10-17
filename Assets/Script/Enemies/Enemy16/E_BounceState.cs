using UnityEngine;

public class E_BounceState : EnemiesState
{
    private Enemy16 enemy;
    private bool firstBounce;
    private bool isAnimationTriggered = false;
    public E_BounceState(Entity entity, StateManager stateManager, string animBoolName, Enemy16 enemy) : base(entity, stateManager, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.direction = Vector2.right;
        Movement.SetVelocity(enemy.speed, enemy.direction);
        enemy.Animator.SetBool("idle2", true);
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
        Movement.SetVelocity(enemy.speed, enemy.direction);
        Bouncing();
    }

    private void Bouncing()
    {
        Collider2D hit = Physics2D.OverlapCircle((Vector2)enemy.transform.position + enemy.direction * enemy.detectionRadius, enemy.detectionRadius, enemy.entityData.whatIsGround);

        if (hit != null)
        {
            // Reflect the direction based on the obstacle's position

            Vector2 contactPoint = hit.ClosestPoint(enemy.transform.position);
            Vector2 normal = (contactPoint - (Vector2)enemy.transform.position).normalized;
            enemy.direction = Vector2.Reflect(enemy.direction, normal).normalized;

            // Toggle bounce animation
            if (firstBounce)
            {
                enemy.Animator.SetTrigger("hitwall2");
               
            }
            else
            {
                enemy.Animator.SetTrigger("hitwall1");
               
            }
            enemy.Animator.SetBool("idle2", firstBounce);
            // Toggle the first bounce flag for alternating animations
            firstBounce = !firstBounce;

            // Ensure animation triggers only once per bounce
            isAnimationTriggered = true;
        }
    }
}
