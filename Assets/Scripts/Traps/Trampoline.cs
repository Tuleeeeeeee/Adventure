using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private int baseBounce;
    [SerializeField] private float extraBoost;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody == null) return;

        // Check if object is falling onto the trampoline
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        // Optional: only bounce if falling
        if (rb.velocity.y <= 0f)
        {
            animator.SetTrigger("Bounce");
            float fallSpeed = Mathf.Abs(rb.velocity.y);
            float bounce = baseBounce + fallSpeed * extraBoost;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        float assumedMass = 10f;

        float gravity = Mathf.Abs(Physics2D.gravity.y * 4f);
        float launchVelocity = baseBounce / assumedMass;

        float height = launchVelocity * launchVelocity / (2 * gravity);

        Gizmos.color = Color.cyan;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * height;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(end, 1f);
    }
#endif
}
