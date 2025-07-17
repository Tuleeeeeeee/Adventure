using UnityEngine;

[RequireComponent(typeof(WaypointFollower))]
public class ThwompTrap : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private LayerMask whatIsGround;
    private WaypointFollower waypointFollower;
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private float movementThreshold = 0.1f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 previousPosition;
    private readonly Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        waypointFollower = GetComponent<WaypointFollower>();
        previousPosition = rb.position;
    }
    void Update()
    {
        animator.SetBool("isBlink", waypointFollower.IsWaiting);
    }
    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;
        Vector2 movement = currentPosition - previousPosition;

        if (movement.magnitude >= movementThreshold)
            CheckCollisionDirection(movement.normalized);

        previousPosition = currentPosition;
    }
    private void CheckCollisionDirection(Vector2 direction)
    {
        bool horizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        if (horizontal)
        {
            if (direction.x > 0 && IsTouchingWall(Vector2.right))
                TriggerDirection("Right");
            else if (direction.x < 0 && IsTouchingWall(Vector2.left))
                TriggerDirection("Left");
        }
        else
        {
            if (direction.y > 0 && IsTouchingWall(Vector2.up))
                TriggerDirection("Top");
            else if (direction.y < 0 && IsTouchingWall(Vector2.down))
                TriggerDirection("Down");
        }
    }
    private bool IsTouchingWall(Vector2 dir)
    {
        return Physics2D.Raycast(transform.position, dir, checkDistance, whatIsGround);
    }
    private void TriggerDirection(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Vector2 dir in directions)
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(dir * checkDistance));
        }
    }
#endif
}
