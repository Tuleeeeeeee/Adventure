using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    [Header("Spring Settings")]
    //[SerializeField] private float springForce = 20f; // How high the player bounces
    [SerializeField] private float platformDropDistance = 0.5f; // How far platform drops
    [SerializeField] private float dropSpeed = 10f; // How fast platform drops
    [SerializeField] private float returnSpeed = 3f; // How fast platform returns
    [SerializeField] private float cooldownTime = 0.5f; // Time before platform can be used again

    private Vector2 originalPosition;
    private bool isDropping = false;
    private bool isReturning = false;
    private bool isCoolingDown = false;
    private float currentCooldown = 0f;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isDropping)
        {
            // Move platform downward
            transform.position = Vector2.MoveTowards(transform.position, 
                originalPosition + (Vector2.down * platformDropDistance), 
                dropSpeed * Time.deltaTime);

            // Check if reached drop position
            if (Vector2.Distance(transform.position, originalPosition + (Vector2.down * platformDropDistance)) < 0.01f)
            {
                isDropping = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            // Move platform back up
            transform.position = Vector2.MoveTowards(transform.position, 
                originalPosition, 
                returnSpeed * Time.deltaTime);

            // Check if returned to original position
            if (Vector2.Distance(transform.position, originalPosition) < 0.01f)
            {
                isReturning = false;
                isCoolingDown = true;
            }
        }
        else if (isCoolingDown)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= cooldownTime)
            {
                isCoolingDown = false;
                currentCooldown = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDropping && !isReturning && !isCoolingDown)
        {
            // Check if player is coming from above
            if (collision.relativeVelocity.y <= 0)
            {
                // // Launch player upward
                // Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                // if (playerRb != null)
                // {
                //     playerRb.velocity = new Vector2(playerRb.velocity.x, springForce);
                // }

                // Start platform drop
                isDropping = true;
            }
        }
    }
}