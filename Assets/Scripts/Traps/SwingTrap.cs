using Tuleeeeee.Enums;
using UnityEngine;

public class SwingTrap : MonoBehaviour
{

    [SerializeField] Transform spikeBall;
    [SerializeField] private SwingType swingType;
    [SerializeField] private CircularMovementOrientation circularType;

    [SerializeField][Range(0, 180)] private float maxSwingAngle = 45f;
    [SerializeField] private float swingSpeed = 2f;         // Speed of swinging
    [SerializeField] private float swingLength = 2f;        // Length of the rope or chain (distance from the pivot)
    [SerializeField] private float rotationSpeed = 100f;    // Speed of spike ball rotation around its own axis
    private float swingTime;
    private float rotationAngle;

    void Start()
    {
        if (spikeBall != null)
            spikeBall.localPosition = new Vector3(0, -swingLength, 0);
    }
    void Update()
    {
        swingTime += Time.deltaTime * swingSpeed;
        UpdateSwingRotation();
        UpdateSpikeBallRotation();
    }
    private void UpdateSwingRotation()
    {
        float swingAngle = 0f;

        switch (swingType)
        {
            case SwingType.Normal:
                swingAngle = maxSwingAngle * Mathf.Sin(swingTime);
                break;
            case SwingType.Loop:
                switch (circularType)
                {
                    case CircularMovementOrientation.Clockwise:
                        swingAngle = Mathf.Repeat(-swingTime * maxSwingAngle, 360f);
                        break;
                    case CircularMovementOrientation.Counterclockwise:
                        swingAngle = Mathf.Repeat(swingTime * maxSwingAngle, 360f);
                        break;
                }
                break;
        }

        transform.localRotation = Quaternion.Euler(0, 0, swingAngle);
    }
    private void UpdateSpikeBallRotation()
    {
        if (spikeBall == null) return;

        rotationAngle += rotationSpeed * Time.deltaTime;
        spikeBall.localRotation = Quaternion.Euler(0, 0, rotationAngle);
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (spikeBall == null) return;

        Vector3 pivot = transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pivot, 0.1f); // Pivot sphere

        if (swingType == SwingType.Normal)
        {
            Gizmos.color = Color.red;
            int segments = 20;
            Vector3 prevPoint = pivot + Quaternion.Euler(0, 0, -maxSwingAngle) * Vector3.down * swingLength;

            for (int i = 1; i <= segments; i++)
            {
                float angle = Mathf.Lerp(-maxSwingAngle, maxSwingAngle, i / (float)segments);
                Vector3 point = pivot + Quaternion.Euler(0, 0, angle) * Vector3.down * swingLength;
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, spikeBall.position);

        Gizmos.color = Color.green;
        Vector3 dynamicSwing = pivot + Quaternion.Euler(0, 0, Mathf.Sin(swingTime) * maxSwingAngle) * Vector3.down * swingLength;
        Gizmos.DrawWireSphere(dynamicSwing, 0.1f); // Current position sphere
    }
#endif
}


