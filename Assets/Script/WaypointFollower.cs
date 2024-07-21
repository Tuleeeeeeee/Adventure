using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private GameObject chainPoint;

    [SerializeField]
    private float Delta;

 

    private int currentWaypointIndex = 0;

    private bool isMovingForward;

    private void Start()
    {
        if (chainPoint != null)
        {
            CreateChain();
        }
    }
    void Update()
    {
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

            // Handle looping if reaching the end while moving forward
            /*if (isMovingForward && currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 1;
            }*/


        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
     
    }
    private void CreateChain()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints.Length > i + 1)
            {
                Vector2 startPos = new Vector2(waypoints[i].transform.position.x, waypoints[i].transform.position.y);
                Vector2 endPos = new Vector2(waypoints[i + 1].transform.position.x, waypoints[i + 1].transform.position.y);
                Vector2 point = startPos;
                Vector2 direction = (endPos - startPos).normalized;

                while ((endPos - startPos).magnitude > (point - startPos).magnitude)
                {
                    Instantiate(chainPoint, point, Quaternion.identity, transform.parent);
                    point += (direction * Delta);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, waypoints[currentWaypointIndex].transform.position);
    }
}
