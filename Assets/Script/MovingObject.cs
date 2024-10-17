using DG.Tweening;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private LayerMask whatIsGround;


    private int currentWaypointIndex = 0;
    //private bool isMovingForward = true;


    [SerializeField]
    private float checkDistance;

    private Tween moveTween;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }
    private void Update()
    {
        determineDirection();
    }

    private void MoveToNextWaypoint()
    {
        // Calculate the duration based on the distance and speed
        float duration = (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) / speed);

        moveTween = transform.DOMove(waypoints[currentWaypointIndex].transform.position, duration).SetEase(Ease.InQuart).OnComplete(() =>
        {
            currentWaypointIndex++;

            // If we reached the last waypoint, loop back to the first one
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }

            MoveToNextWaypoint();
        });

    }
    private void determineDirection()
    {
        Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Handle horizontal movement
            if (direction.x > 0 && isTouching(Vector2.right))
                handleCollisionAnimator("Right");
            else if (direction.x < 0 && isTouching(Vector2.left))
                handleCollisionAnimator("Left");
        }
        else
        {
            // Handle vertical movement
            if (direction.y > 0 && isTouching(Vector2.up))
                handleCollisionAnimator("Top");
            else if (direction.y < 0 && isTouching(Vector2.down))
                handleCollisionAnimator("Down");
        }
    }

    private bool isTouching(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, checkDistance, whatIsGround);
    }
    private void handleCollisionAnimator(string direction)
    {
        animator.SetTrigger(direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(checkDistance * Vector2.up));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(checkDistance * Vector2.down));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(checkDistance * Vector2.left));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(checkDistance * Vector2.right));
    }
}

