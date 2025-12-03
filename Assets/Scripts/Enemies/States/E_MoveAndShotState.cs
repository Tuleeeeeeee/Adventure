using UnityEngine;

public class E_MoveAndShotState : E_AttackState
{
    protected D_MoveAndAttack stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;

    private Vector2 currentPosition;
    private Vector2 center;

    public E_MoveAndShotState(Entity entity, StateManager stateManager, string animBoolName, D_MoveAndAttack stateData,
        Transform attackPosition) : base(entity, stateManager, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        projectile = Object.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed * Movement.FacingDirection,
            stateData.projectileTravelDistance, stateData.projectileDamage);
    }

    public override void Enter()
    {
        base.Enter();
        center = Entity.transform.position;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        stateData.angle += stateData.speed * Time.deltaTime;
        Vector2 currentEllipse = stateData.ellipses[stateData.currentPath];

        float a = currentEllipse.x;
        float b = currentEllipse.y;
        float x = a * Mathf.Cos(stateData.angle);
        float y = b * Mathf.Sin(stateData.angle);

        stateData.targetPosition = new Vector2(x, y) + center;
        currentPosition = Entity.transform.position;
        Vector2 velocity = (stateData.targetPosition - currentPosition) / Time.deltaTime;

        //Movement?.SetVelocityX(velocity.x);
        Movement?.SetVelocityY(velocity.y);

        if (stateData.angle >= Mathf.PI * 2)
        {
            stateData.angle = 0f; // Reset angle to start at the beginning of the next ellipse
            stateData.currentPath = (stateData.currentPath + 1) % stateData.ellipses.Length; // Move to the next ellipse
        }
    }
}