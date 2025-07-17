using Tuleeeeee.Enums;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Moving Platform Attribute")]
    [SerializeField] private AnimationCurve easeCurve;
    public PlatformMovementType movementType;
    public PlatformTriggerType triggerType;
    [SerializeField] private bool isPlatform;
    [SerializeField] private bool isLooping = true; // If true, the platform will loop back to the first waypoint after reaching the last one
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float stopDuration;
    private float stopTimer;
    public bool IsWaiting{ get; private set; } 
    private bool isMovingForward;
    private bool isActivated = false;
    private bool isReturningToFirstPoint = false;

    #region EASE_CURVE
    private float moveProgress = 0f;
    private float moveDuration;
    private Vector2 startPoint;
    private Vector2 endPoint;
    #endregion

    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Set to Kinematic for controlled
        if (waypoints.Length < 2)
        {
            Debug.LogError("WaypointFollower requires at least two waypoints to function properly.");
            enabled = false;
            return;
        }
        rb.position = waypoints[0].position;
        // Initialize movement ONLY if automatic trigger is enabled
        if (triggerType == PlatformTriggerType.Automatic)
        {
            isActivated = true;
            InitializeMovement(); // <--- only here
            if (animator != null && isPlatform)
                animator.SetBool("isActivated", isActivated);
        }
    }
    private void FixedUpdate()
    {
        if (!isActivated && !isReturningToFirstPoint) return;

        if (isReturningToFirstPoint)
        {
            ReturnToStart();
        }
        else
        {
            switch (movementType)
            {
                case PlatformMovementType.Line:
                    MoveLine();
                    break;
            }
        }

    }
    private void InitializeMovement()
    {
        startPoint = rb.position; // use current position
        endPoint = waypoints[GetNextWaypointIndex()].position;
        moveDuration = CalculateMoveDuration(startPoint, endPoint);
        moveProgress = 0f;
    }
    private float CalculateMoveDuration(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        return Mathf.Max(distance / Mathf.Max(speed, 0.001f), 0.01f);
    }
    private int GetNextWaypointIndex()
    {
        if (isLooping)
        {
            return (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            if (isMovingForward)
            {
                if (currentWaypointIndex + 1 >= waypoints.Length)
                    return waypoints.Length - 2;
                return currentWaypointIndex + 1;
            }
            else
            {
                if (currentWaypointIndex - 1 < 0)
                    return 1;
                return currentWaypointIndex - 1;
            }
        }
    }
    private void UpdateWaypointIndex()
    {
        if (isLooping)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first
            }
        }
        else
        {
            if (isMovingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    isMovingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    isMovingForward = true;
                }
            }
        }
    }
    private void MoveLine()
    {
        if (IsWaiting)
        {
            stopTimer += Time.fixedDeltaTime;
            if (stopTimer >= stopDuration)
            {
                IsWaiting = false;
                stopTimer = 0f;
                UpdateWaypointIndex();
                InitializeMovement();
                if (animator != null && isPlatform)
                    animator.SetBool("isActivated", true);
            }
            return;
        }

        moveProgress += Time.fixedDeltaTime / moveDuration;
        float easedT = easeCurve.Evaluate(Mathf.Clamp01(moveProgress));

        Vector2 newPosition = Vector2.Lerp(startPoint, endPoint, easedT);
        rb.MovePosition(newPosition);

        if (moveProgress >= 1f)
        {
            rb.MovePosition(endPoint); 
            if (animator != null && isPlatform)
                animator.SetBool("isActivated", false);
            IsWaiting = true;
        }
    }
    private void ReturnToStart()
    {
        moveProgress += Time.fixedDeltaTime / moveDuration;
        float easedT = easeCurve.Evaluate(Mathf.Clamp01(moveProgress));

        Vector2 newPosition = Vector2.Lerp(startPoint, endPoint, easedT);
        rb.MovePosition(newPosition);

        if (moveProgress >= 1f)
        {
            rb.position = endPoint;
            isReturningToFirstPoint = false;
            isActivated = false;
            currentWaypointIndex = 0;
            isMovingForward = true;
            moveProgress = 0f;

            if (animator != null && isPlatform)
            {
                animator.SetBool("isActivated", isActivated);
            }
        }
    }


    #region ON_COLLISION
    // Handle collision with player to activate the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActivated && triggerType == PlatformTriggerType.PlayerTrigger && collision.collider.CompareTag("Player"))
        {
            isActivated = true;
            isReturningToFirstPoint = false;
            moveProgress = 0f; // Reset movement
            InitializeMovement();
            if (animator != null && isPlatform)
            {
                animator.SetBool("isActivated", isActivated);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (triggerType == PlatformTriggerType.PlayerTrigger)
            {
                isReturningToFirstPoint = true;
                moveProgress = 0f; // Reset return movement
                startPoint = rb.position;
                endPoint = waypoints[0].position;
                moveDuration = CalculateMoveDuration(startPoint, endPoint);
                if (animator != null && isPlatform)
                {
                    animator.SetBool("isActivated", isActivated);
                }
            }
        }
    }
    #endregion
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            Gizmos.DrawSphere(waypoints[i].position, 0.1f);
        }

        Gizmos.DrawSphere(waypoints[waypoints.Length - 1].position, 0.1f);

        if (isLooping)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint, 0.15f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(endPoint, 0.15f);

    }
#endif
}