using UnityEngine;

public class Test : MonoBehaviour
{
    public enum SwingType
    {
        Normal,
        Loop,
        LoopReverse,
    }
    public Transform spikeBall;    // Reference to the spike ball object
    [SerializeField]
    private SwingType swingType;
    [SerializeField]
    [Range(0, 180)]
    private float maxSwingAngle = 45f;     // Maximum swing angle (in degrees)
    public float swingSpeed = 2f;         // Speed of swinging
    public float swingLength = 2f;        // Length of the rope or chain (distance from the pivot)
    public float rotationSpeed = 100f;    // Speed of spike ball rotation around its own axis

    public float chainLinkSize = 0.5f;

    private float swingTime;
    private float rotationAngle;
    [SerializeField]
    private GameObject chain;

    void Start()
    {
        // Position the spike ball at the desired swing length (below the pivot point in 2D space)
        spikeBall.localPosition = new Vector3(0, -swingLength, 0);
        CreateChains(CalculateChainLinks());
    }

    void Update()
    {
        // Increment time to simulate swinging over time
        swingTime += Time.deltaTime * swingSpeed;

        // Calculate the swing angle using a sine wave
        float swingAngle = maxSwingAngle * Mathf.Sin(swingTime);

        float swingAngleLoop = maxSwingAngle * swingTime;

        switch (swingType)
        {
            case SwingType.Normal:
                transform.localRotation = Quaternion.Euler(0, 0, swingAngle);
                break;
            case SwingType.Loop:
                transform.localRotation = Quaternion.Euler(0, 0, swingAngleLoop);
                break;
            case SwingType.LoopReverse:
                transform.localRotation = Quaternion.Euler(0, 0, -swingAngleLoop);
                break;
        }
        // Increment rotation angle for continuous rotation
        rotationAngle += rotationSpeed * Time.deltaTime;

        // Apply continuous rotation to the spike ball
        spikeBall.localRotation = Quaternion.Euler(0, 0, rotationAngle);
    }
    private int CalculateChainLinks()
    {
        // Calculate the distance between the pivot and the spike ball
        float distance = Vector3.Distance(transform.position, spikeBall.position);

        // Calculate the number of chain links required by dividing the distance by the size of one chain link
        int calculatedLinks = Mathf.CeilToInt(distance / chainLinkSize);

        // Return the calculated number of chain links
        return calculatedLinks;
    }

    private void CreateChains(int numOfLinks)
    {
        // Clear any existing chains
        /* foreach (Transform child in transform)
         {
             Destroy(child.gameObject);
         }*/

        // Calculate the distance between the pivot and the spike ball
        Vector3 startPosition = transform.position;
        Vector3 endPosition = spikeBall.position;

        // Instantiate chain links between the pivot and spike ball
        for (int i = 0; i < numOfLinks - 2; i++)
        {
            // Calculate the position of each chain link using Lerp
            float t = (float)i / (numOfLinks - 1); // t varies between 0 and 1
            Vector3 chainPosition = Vector3.Lerp(startPosition, endPosition, t);

            // Instantiate the chain prefab at the calculated position
            GameObject newChainLink = Instantiate(chain, chainPosition, Quaternion.identity);

            // Set the chain's parent to the chain pivot for hierarchical organization
            newChainLink.transform.SetParent(transform);
        }
    }

    void OnDrawGizmos()
    {
        // Ensure we have a reference to the spike ball
        if (spikeBall == null) return;

        // Draw the pizza slice for the swing arc
        Gizmos.color = Color.red;
        Vector3 pivot = transform.position;
        /*float angleStep = 1f;*/ // Smaller steps for a smoother arc
        int segments = 20;//Mathf.CeilToInt((maxSwingAngle/2) / angleStep);

        // Draw the slice shape
        Vector3 prevPoint = pivot + Quaternion.Euler(0, 0, -maxSwingAngle) * Vector3.down * swingLength;
        // Connect the last point back to the pivot
        Gizmos.DrawLine(prevPoint, pivot);

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Lerp(-maxSwingAngle, maxSwingAngle, i / (float)segments);
            Vector3 point = pivot + Quaternion.Euler(0, 0, angle) * Vector3.down * swingLength;
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }

        Gizmos.DrawLine(prevPoint, pivot);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, spikeBall.position);

        // Draw a small sphere at the pivot point
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pivot, 0.1f); // Draw a small sphere at the pivot point

        // Draw the pivot of the spike ball
        Gizmos.color = Color.green;
        Vector3 spikeBallPivot = pivot + Quaternion.Euler(0, 0, Mathf.Sin(swingTime) * maxSwingAngle) * Vector3.down * swingLength;
        Gizmos.DrawWireSphere(spikeBallPivot, 0.1f);
    }
}


