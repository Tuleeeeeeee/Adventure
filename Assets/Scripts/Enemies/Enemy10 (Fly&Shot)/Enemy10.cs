using UnityEngine;
public class Enemy10 : Entity
{
    public E10_MoveAndShotState MoveState { get; private set; }

    [SerializeField]
    private D_MoveAndAttack moveAndAttackStateData;

    [SerializeField]
    private Transform rangedAttackPosition;

    [SerializeField]
    private float checkRadius;

    private Vector2 ellipseCenter;
    public override void Awake()
    {
        base.Awake();
        MoveState = new E10_MoveAndShotState(this, StateManager, "move", moveAndAttackStateData, rangedAttackPosition, this);
    }
    public override void Start()
    {
        base.Start();
        ellipseCenter = new Vector2(transform.position.x, transform.position.y);
        StateManager.InitializeEnemy(MoveState);
    }
    public override bool CheckIfCanJump() => false;

    public override bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, entityData.whatIsPlayer);
    }
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        // Set Gizmos color for the ellipse paths
        Gizmos.color = Color.cyan;

        for (int i = 0; i < moveAndAttackStateData.ellipses.Length; i++)
        {
            DrawEllipse(moveAndAttackStateData.ellipses[i], ellipseCenter);
        }
    }
    // Method to draw an ellipse using the parametric equation
    private void DrawEllipse(Vector2 ellipse, Vector2 center)
    {
        int segments = 20; // Number of segments for smoother ellipses
        float angleStep = 2 * Mathf.PI / segments; // Step size for each segment
        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float theta = i * angleStep;
            float x = center.x + ellipse.x * Mathf.Cos(theta); // Parametric equation x = a * cos(theta)
            float y = center.y + ellipse.y * Mathf.Sin(theta); // Parametric equation y = b * sin(theta)
            Vector3 currentPoint = new Vector3(x, y, 0f);

            // Draw line between the previous point and the current point
            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, currentPoint);
            }

            prevPoint = currentPoint; // Update previous point
        }
    }
}
