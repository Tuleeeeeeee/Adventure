using UnityEngine;

public class E_FlyState : EnemiesState
{
    D_FlyState stateData;
    protected Vector2 currentPosition;
    protected Vector2 center;

    public E_FlyState(Entity entity, StateManager stateManager, string animBoolName, D_FlyState stateData) : base(
        entity, stateManager, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.Rb.gravityScale = 0f;
        stateData.targetPosition = Vector2.zero;
        center = Entity.transform.position;
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // Update the angle over time to move the object
        stateData.angle += stateData.speed * Time.deltaTime;
        // Get the current ellipse (a and b values)
        Vector2 currentEllipse = stateData.ellipses[stateData.currentPath];
        float a = currentEllipse.x; // Semi-major axis
        float b = currentEllipse.y; // Semi-minor axis
        // Calculate the new target position using the parametric equation of the current ellipse
        float x = a * Mathf.Cos(stateData.angle);
        float y = b * Mathf.Sin(stateData.angle);

        // Set the target position
        stateData.targetPosition = new Vector2(x, y) + center;
        currentPosition = Entity.transform.position;
        // Calculate the velocity needed to reach the target position
        Vector2 velocity = (stateData.targetPosition - currentPosition) / Time.deltaTime;

        Movement?.SetVelocityX(velocity.x);
        Movement?.SetVelocityY(velocity.y);

        if (Movement?.CurrentVelocity.x > 0 && Movement.FacingDirection == -1)
        {
            Movement?.Flip();
        }
        else if (Movement?.CurrentVelocity.x < 0 && Movement.FacingDirection == 1)
        {
            Movement?.Flip();
        }
    }
}