
using UnityEngine;
public class WaypointFollower : MonoBehaviour
{
    public enum PlatformMode
    {
        Manual,      // The platform moves when the player is on it
        Looping,     // The platform moves in a loop between waypoints
        PingPong     // The platform moves back and forth between waypoints
    }
    [Header("Moving Platform Attribute")]
    public PlatformMode platformMode;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isMovingForward;
    private bool isPlayerOnPlatform = false;
    private Animator animator;

    [SerializeField]
    private Vector2 checkBoxSize = new Vector2(1, .3f);
    [SerializeField]
    private LayerMask whatIsPlayer;

    [Header("Chain Create")]
    [SerializeField]
    private GameObject chain;
    [SerializeField]
    private float chainLinkSize = 0.5f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    [Header("TimeWaiting")]
    private bool isWaiting;
    [SerializeField]
    private float stopDuration;
    private float stopTimer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = waypoints[0].position;
        endPosition = waypoints[waypoints.Length - 1].position;

        if (chain != null)
        {
            createChain(calculateChainLinks());
        }
    }
    void Update()
    {
        switch (platformMode)
        {
            case PlatformMode.Manual:
                handleManualMode();
                break;

            case PlatformMode.Looping:
                handleLoopingMode();
                break;

            case PlatformMode.PingPong:
                handlePingPongMode();
                break;
        }
    }
    private void FixedUpdate()
    {
        isPlayerOnPlatform = checkIfPlayerSteppedOn();
    }
    // Handle platform movement when in Manual mode (requires player interaction)
    private void handleManualMode()
    {
        if (isPlayerOnPlatform)
        {
            setAnim(true);
            if (Vector2.Distance(waypoints[currentWaypointIndex].position, transform.position) < .1f)
            {
                // Move to the next waypoint if not at the last one
                if (currentWaypointIndex < waypoints.Length - 1)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    // Stop the platform when it reaches the last waypoint
                    setAnim(false); // Optionally stop the animation
                    return; // Exit the function to stop movement
                }
            }

            // Move the platform
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position,
                Time.deltaTime * speed);
        }
        else
        {
            // Return to the first point when player is not on platform
            if (transform.position != waypoints[0].position)
            {
                currentWaypointIndex = 0;
                moveToFirstPoint();
            }
            else
            {
                setAnim(false); // Stop the animation when it's at the first point
            }
        }
    }
    // Handle looping movement mode (platform loops through waypoints)
    private void handleLoopingMode()
    {
        setAnim(true);
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;

            // If we reached the last waypoint, loop back to the first one
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
    }

    // Handle ping-pong movement mode (platform moves back and forth between waypoints)
    private void handlePingPongMode()
    {
        if (isWaiting)
        {
            setAnim(false);
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isWaiting = false;
            }
            return;
        }

        moveToWaypoint();
    }
    void moveToFirstPoint()
    {
        setAnim(true); // Set the platform to "moving" animation
        // Move to the first point
        transform.position = Vector2.MoveTowards(transform.position, waypoints[0].position, Time.deltaTime * speed);
        // Stop the platform and animation when it reaches the first point
        if (Vector2.Distance(transform.position, waypoints[0].position) < 0.1f)
        {
            setAnim(false); // Stop the animation when it reaches the first point
        }
    }

    private void moveToWaypoint()
    {
        setAnim(true);
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
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

            // Start the stop timer
            isWaiting = true;
            stopTimer = stopDuration;

        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
    }
    private int calculateChainLinks()
    {
        // Calculate the distance between the pivot and the spike ball
        float distance = Vector3.Distance(startPosition, endPosition);

        // Calculate the number of chain links required by dividing the distance by the size of one chain link
        int calculatedLinks = Mathf.CeilToInt(distance / chainLinkSize);

        // Return the calculated number of chain links
        return calculatedLinks;
    }
    private void createChain(int numOfLinks)
    {
        // Instantiate chain links between the pivot and spike ball
        for (int i = 0; i < numOfLinks; i++)
        {
            // Calculate the position of each chain link using Lerp
            float t = (float)i / (numOfLinks - 1); // t varies between 0 and 1
            Vector3 chainPosition = Vector3.Lerp(startPosition, endPosition, t);

            // Instantiate the chain prefab at the calculated position
            GameObject newChainLink = Instantiate(chain, chainPosition, Quaternion.identity);

            // Set the chain's parent to the chain pivot for hierarchical organization
            newChainLink.transform.SetParent(transform.parent);
        }
    }
    private bool checkIfPlayerSteppedOn()
    {
        // Check if the player is inside the overlap box (checking for collisions)
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, checkBoxSize, 90, whatIsPlayer);

        if (playerCollider != null)
        {
            Vector3 playerPosition = playerCollider.transform.position;
            Vector3 platformPosition = transform.position;

            // Check if the player is above the platform and moving downward
            if (playerPosition.y > platformPosition.y)
            {
                // Player is stepping on the platform from above (falling)
                return true;
            }
        }
        // Player is not stepping on the platform
        return false;
    }
    private void setAnim(bool moving)
    {
        if (animator != null)
        {
            animator.SetBool("isOn", moving); // Trigger the moving/idle animation
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, waypoints[currentWaypointIndex].transform.position);
        Gizmos.DrawCube(transform.position, checkBoxSize);
    }
}

