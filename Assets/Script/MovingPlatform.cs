using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
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
    private bool isMovingForward = true;
    private bool isTouchingRight, isTouchingLeft, isTouchingTop, isTouchingBottom;

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
            if (currentWaypointIndex == 0 && !isMovingForward)
            {
                isMovingForward = true;
            }
            else if (currentWaypointIndex == waypoints.Length - 1 && isMovingForward)
            {
                isMovingForward = false;
            }

            // Update index based on direction
            currentWaypointIndex += (isMovingForward ? 1 : -1);

            // Loop back to the beginning if needed
            /*if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Set to 1 because we're reversing direction
                isMovingForward = true;
            }*/
            MoveToNextWaypoint();
        });

    }
    void DetermineDirection()
    {
        Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Moving horizontally
            if (direction.x > 0)
            {
                isTouchingRight = checkIfTouchingRight();
                if (isTouchingRight)
                {
                    // animator.Play("Right_Hit_Anim");
                    animator.SetTrigger("Right");
                    isTouchingRight = false;
                }
            }
            else
            {
                isTouchingLeft = checkIfTouchingLeft();
                if (isTouchingLeft)
                {
                    // animator.Play("Left_Hit_Anim");
                    animator.SetTrigger("Left");
                    isTouchingLeft = false;
                }
            }
        }
        else
        {
            // Moving vertically
            if (direction.y > 0)
            {
                isTouchingTop = checkIfTouchingTop();
                if (isTouchingTop)
                {
                    animator.SetTrigger("Top");
                    isTouchingTop = false;
                    //animator.Play("Top_Hit_Anim");
                }
            }
            else
            {
                isTouchingBottom = checkIfTouchingBottom();
                if (isTouchingBottom)
                {
                    animator.SetTrigger("Down");
                    isTouchingBottom = false;
                    //animator.Play("Bottom_Hit_Anim");
                }
            }
        }
    }
    private bool checkIfTouchingRight()
    {
        return Physics2D.OverlapCircle(right.position, radius, whatIsGround);
    }
    private bool checkIfTouchingLeft()
    {
        return Physics2D.OverlapCircle(left.position, radius, whatIsGround);
    }
    private bool checkIfTouchingTop()
    {
        return Physics2D.OverlapCircle(top.position, radius, whatIsGround);
    }
    private bool checkIfTouchingBottom()
    {
        return Physics2D.OverlapCircle(bottom.position, radius, whatIsGround);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Mlem");
            DetermineDirection();
            moveTween.Kill(); // Stop the current movement

            DOVirtual.DelayedCall(.5f, () =>
            {
                MoveToNextWaypoint(); // Continue moving in the new direction after delay
            });
        }
    }*//*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Mlem");
        }
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(left.position, radius);
        Gizmos.DrawSphere(right.position, radius);
        Gizmos.DrawSphere(top.position, radius);
        Gizmos.DrawSphere(bottom.position, radius);
    }
}

