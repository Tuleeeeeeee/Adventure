using UnityEngine;

public class Enemy11 : Entity
{
    public E11_IdleState IdleState { get; private set; }
    public E11_MoveState MoveState { get; private set; }
    public E11_FlyState FlyState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_FlyState flyStateData;

    public Transform leafPos;

    public GameObject leafPart1;
    public GameObject leafPart2;

    private Vector2 ellipseCenter;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E11_MoveState(this, StateManager, "move", moveStateData, this);

        IdleState = new E11_IdleState(this, StateManager, "idle", idleStateData, this);

        FlyState = new E11_FlyState(this, StateManager, "fly", flyStateData, this);

    }
    public override void Start()
    {
        base.Start();

        ellipseCenter = transform.position;

        StateManager.InitializeEnemy(FlyState);

    }

    public override bool CheckPlayerInMinAgroRange() => false;

    public override bool CheckPlayerInMaxAgroRange() => false;

    public override bool CheckPlayerInCloseRangeAction() => false;
    public override bool CheckIfCanJump() => false;
    public void SplitLeaf()
    {

        GameObject part1 = Instantiate(leafPart1, leafPos.position, leafPos.rotation);
        Rigidbody2D rb1 = part1.GetComponent<Rigidbody2D>();
        rb1.velocity = new Vector2(-1, 1).normalized * 5; // Adjust direction as needed
        Destroy(part1, 5f);


        GameObject part2 = Instantiate(leafPart2, leafPos.position, leafPos.rotation);
        Rigidbody2D rb2 = part2.GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(1, 1).normalized * 5; // Adjust direction as needed
        Destroy(part2, 5f);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        // Set Gizmos color for the ellipse paths
        Gizmos.color = Color.blue;

        for (int i = 0; i < flyStateData.ellipses.Length; i++)
        {
            DrawEllipse(flyStateData.ellipses[i], ellipseCenter);
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
