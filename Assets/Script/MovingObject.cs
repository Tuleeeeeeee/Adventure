using DG.Tweening;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    Transform left, right, top, bottom;
    [SerializeField]
    private LayerMask whatIsGround;


    private int currentWaypointIndex = 0;
    //private bool isMovingForward = true;


    [SerializeField]
    private float radius;

    private Tween moveTween;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }
    private void Update()
    {
        DetermineDirection();
    }

    private void MoveToNextWaypoint()
    {
        // Calculate the duration based on the distance and speed
        float duration = (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) / speed);

        moveTween = transform.DOMove(waypoints[currentWaypointIndex].transform.position, duration).SetEase(Ease.InQuart).OnComplete(() =>
        {
            // Change direction at the end/beginning
            /* if (currentWaypointIndex == 0 && !isMovingForward)
             {
                 isMovingForward = true;
             }
             else if (currentWaypointIndex == waypoints.Length - 1 && isMovingForward)
             {
                 isMovingForward = false;
             }

             // Update index based on direction
             currentWaypointIndex += (isMovingForward ? 1 : -1);*/

            // Loop back to the beginning if needed
            /*if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Set to 1 because we're reversing direction
                isMovingForward = true;
            }*/

            currentWaypointIndex++;

            // If we reached the last waypoint, loop back to the first one
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }

            MoveToNextWaypoint();
        });

    }
    void DetermineDirection()
    {
        Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Handle horizontal movement
            if (direction.x > 0 && IsTouching(right))
                HandleCollision("Right");
            else if (direction.x < 0 && IsTouching(left))
                HandleCollision("Left");
        }
        else
        {
            // Handle vertical movement
            if (direction.y > 0 && IsTouching(top))
                HandleCollision("Top");
            else if (direction.y < 0 && IsTouching(bottom))
                HandleCollision("Down");
        }
    }

    private bool IsTouching(Transform directionTransform)
    {
        return Physics2D.OverlapCircle(directionTransform.position, radius, whatIsGround);
    }
    private void HandleCollision(string direction)
    {
        animator.SetTrigger(direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(left.position, radius);
        Gizmos.DrawSphere(right.position, radius);
        Gizmos.DrawSphere(top.position, radius);
        Gizmos.DrawSphere(bottom.position, radius);
    }
}

