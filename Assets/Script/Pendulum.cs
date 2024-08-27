using UnityEngine;
public class Pendulum : MonoBehaviour
{
    private float angle;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private float length = 3f;

    private float angleV = 0;
    private float angleA = 0;


    private Vector2 bob;
    [SerializeField]
    private float gravity = 1f;
    private void Start()
    {
        angle = Mathf.PI / 4;
    }
    private void FixedUpdate()
    {
        var force = gravity * Mathf.Sin(angle);
        angleA = (-1 * force) / length;
        angleV += angleA;
        angle += angleV;

        bob = new Vector2(length * Mathf.Sin(angle) + origin.position.x,
        Mathf.Cos(angle) + origin.position.y);
        /*bob.x = length * Mathf.Sin(angle) + origin.position.x;
        bob.y = length * Mathf.Cos(angle) + origin.position.y;*/

        transform.position = bob;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, origin.position);

        Gizmos.color = Color.black;

        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}



