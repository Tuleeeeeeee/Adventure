using System.Collections;
using UnityEngine;
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1.5f;
    [SerializeField] private float deactiveDelay = 2.5f;
    [SerializeField] private float respawnTime = 3.5f;

    private Vector3 originalPosition;
    private Rigidbody2D rb;
    private Animator animator;


    private bool isFalling = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("Off");
        yield return new WaitForSeconds(deactiveDelay);
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = originalPosition;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        animator.SetTrigger("On");
        isFalling = false;
    }
    #if UNITY_EDITOR
        // Draw gizmo to show original position in editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(originalPosition, GetComponent<Collider2D>().bounds.size);
        }
    #endif
}