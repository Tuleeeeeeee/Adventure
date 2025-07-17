using Tuleeeeee.Enums;
using UnityEngine;
// Different types of movements for the platform


public class MovingPlatform : MonoBehaviour
{

    // General vars (affect all types of movemens)

    [SerializeField] PlatformMovementType movementType;
    [SerializeField] private float speed = 2f;

    private Vector3 startPosition;
    private Color gizmoColor = Color.yellow;

    // Line movement vars
    [SerializeField] private LineMovementOrientation lineMovementOrientation;
    [SerializeField] private float lineDistance = 5f;

    // Circle movement vars
    [SerializeField] private CircularMovementOrientation circularMovementOrientation;
    [SerializeField] private float circleRadius = 5f;

    // Zigzag movement vars
    [SerializeField] private int zigzagLines = 4;
    [SerializeField] private float zigzagLineDistance = 2;

    private float zigzagStep;
    private bool zigzagMovingPositive = true;

    private Rigidbody2D rb;

    void Start()
    {
        // Set start position
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        zigzagStep = 0f;
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = CalculateTargetPosition();
        Vector2 movement = (targetPosition - (Vector2)transform.position) / Time.fixedDeltaTime;
        rb.velocity = movement;
    }
    private Vector2 CalculateTargetPosition()
    {
        switch (movementType)
        {
            case PlatformMovementType.Line:
                return CalculateLinePosition();
            case PlatformMovementType.Circular:
                return CalculateCirclePosition();
            case PlatformMovementType.Zigzag:
                return CalculateZigzagPosition();
            default:
                return transform.position;
        }
    }
    // Move the platform in a straight line in movementOrientation
    public Vector2 CalculateLinePosition()
    {
        // Get current coordenates 
        float x = startPosition.x;
        float y = startPosition.y;

        // Calculating next position according to the orientation selected
        switch (lineMovementOrientation)
        {
            case LineMovementOrientation.Horizontal:
                x = startPosition.x + Mathf.Sin(Time.time * speed) * lineDistance;
                break;
            case LineMovementOrientation.Vertical:
                y = startPosition.y + Mathf.Sin(Time.time * speed) * lineDistance;
                break;
        }

        // Moving platform
        // this.transform.position = new Vector3(x, y);
        return new Vector2(x, y);
    }

    public Vector2 CalculateCirclePosition()
    {
        // Calculating direction (CW or CCW)
        int direction = (circularMovementOrientation == CircularMovementOrientation.Counterclockwise) ? 1 : -1;

        // Calculating coordenates 
        float x = startPosition.x + Mathf.Cos(Time.time * speed * direction) * circleRadius;
        x -= circleRadius;
        float y = startPosition.y + Mathf.Sin(Time.time * speed * direction) * circleRadius;
        //  float z = transform.position.z;

        // Moving Platform
        //this.transform.position = new Vector3(x, y, z);
        return new Vector2(x, y);
    }


    public Vector2 CalculateZigzagPosition()
    {
        // Changing direction when the platform reach the limits 
        if (transform.position.x >= startPosition.x + zigzagLineDistance * zigzagLines)
        {
            zigzagMovingPositive = false;
        }
        else if (transform.position.x <= startPosition.x)
        {
            zigzagMovingPositive = true;
        }

        // Calculating coordenates
        float factor = (Mathf.Acos(Mathf.Cos(zigzagStep * (float)Mathf.PI)) / (float)Mathf.PI);
        float x = startPosition.x + zigzagStep * zigzagLineDistance;
        float y = startPosition.y + factor * zigzagLineDistance;

        if (zigzagMovingPositive)
        {
            zigzagStep += speed / 50;
        }
        else
        {
            zigzagStep -= speed / 50;
        }

        // keep platform within the limits
        zigzagStep = Mathf.Clamp(zigzagStep, 0, zigzagLines);


        // Moving platform 
        //  this.transform.position = new Vector3(x, y);
        return new Vector2(x, y);
    }

#if UNITY_EDITOR
    // Funtion to see the platform path (only for debugging)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Vector3 src = Vector3.zero;
        Vector3 dest = Vector3.zero;


        switch (movementType)
        {
            case PlatformMovementType.Line:
                if (lineMovementOrientation == LineMovementOrientation.Horizontal)
                {
                    src = new Vector3(startPosition.x - lineDistance, startPosition.y);
                    dest = new Vector3(startPosition.x + lineDistance, startPosition.y);
                    Gizmos.DrawLine(src, dest);
                }
                else if (lineMovementOrientation == LineMovementOrientation.Vertical)
                {
                    src = new Vector3(startPosition.x, startPosition.y - lineDistance);
                    dest = new Vector3(startPosition.x, startPosition.y + lineDistance);
                    Gizmos.DrawLine(src, dest);
                }
                break;
            case PlatformMovementType.Circular:
                // Cicular movement 
                src = new Vector3(startPosition.x - circleRadius, startPosition.y);
                Gizmos.DrawWireSphere(src, circleRadius);
                break;
            case PlatformMovementType.Zigzag:
                float x = startPosition.x;
                float y = startPosition.y;

                for (int i = 0; i < zigzagLines; i++)
                {

                    // Current position
                    src = new Vector3(x, y);

                    // Calculating next position
                    x += zigzagLineDistance;

                    // If "i" is even draw line going up, else, draw line going down
                    // the zigzag movement always start going up
                    y = (i % 2 == 0) ? startPosition.y + zigzagLineDistance : startPosition.y;

                    dest = new Vector3(x, y);

                    // Drawing line
                    Gizmos.DrawLine(src, dest);

                }
                break;
        }
#endif
    }
}
