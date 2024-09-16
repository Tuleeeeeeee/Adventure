using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private bool swingLoop;
    public Transform spikeBall;    // Reference to the spike ball object

    [SerializeField]
    [Range(0, 180)]
    private float maxSwingAngle = 45f;     // Maximum swing angle (in degrees)
    public float swingSpeed = 2f;         // Speed of swinging
    public float swingLength = 2f;        // Length of the rope or chain (distance from the pivot)
    public float rotationSpeed = 100f;    // Speed of spike ball rotation around its own axis

    private float swingTime;
    private float rotationAngle;

    void Start()
    {
        // Position the spike ball at the desired swing length (below the pivot point in 2D space)
        spikeBall.localPosition = new Vector3(0, -swingLength, 0);
    }

    void Update()
    {
        // Increment time to simulate swinging over time
        swingTime += Time.deltaTime * swingSpeed;

        // Calculate the swing angle using a sine wave
        float swingAngle = maxSwingAngle * Mathf.Sin(swingTime);

        float swingAngleLoop = maxSwingAngle * swingTime;

        if (swingLoop)
        {
            transform.localRotation = Quaternion.Euler(0, 0, swingAngleLoop);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, swingAngle);
        }
        // Increment rotation angle for continuous rotation
        rotationAngle += rotationSpeed * Time.deltaTime;

        // Apply continuous rotation to the spike ball
        spikeBall.localRotation = Quaternion.Euler(0, 0, rotationAngle);
    }
    void OnDrawGizmos()
    {
        // Ensure we have a reference to the spike ball
        if (spikeBall == null) return;

        // Draw the pizza slice for the swing arc
        Gizmos.color = Color.red;
        Vector3 pivot = transform.position;
        float angleStep = 1f; // Smaller steps for a smoother arc
        int segments = Mathf.CeilToInt(maxSwingAngle / angleStep);

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


