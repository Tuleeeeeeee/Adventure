using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 1f;
    [SerializeField]
    private float destroyDelay = 2f;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;

    private float timer = 0f;
    private bool startFall = false;

    public float floatAmplitude = 0.5f; // How much the platform floats up and down
    public float floatSpeed = 2.0f;     // Speed of the floating motion
    private Vector3 initialPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's position and compare it with the platform's position
            Vector3 playerPosition = collision.transform.position;
            Vector3 platformPosition = transform.position;

            // Only trigger the fall if the player is above the platform
            if (playerPosition.y > platformPosition.y)
            {
                startFall = true;
                animator.SetTrigger("JumpDown");
            }
        }
    }
        void Start()
        {
            // Save the initial position of the platform
            initialPosition = transform.position;
        }

        void Update()
        {

            if (startFall)
            {
                timer += Time.deltaTime;
                if (timer >= fallDelay)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;  // Make the platform fall
                    Destroy(gameObject, destroyDelay);      // Destroy the platform after a delay
                }
            }
            else
            {
                floating();
            }
        }
        /* private IEnumerator Fall()
         {
             yield return new WaitForSeconds(fallDelay);
             rb.bodyType = RigidbodyType2D.Dynamic;
             Destroy(gameObject, destroyDelay);
         }*/

        private void floating()
        {
            // Apply a sinusoidal wave for smooth floating
            transform.position = new Vector3(
                initialPosition.x,
                initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude,
                initialPosition.z
            );
        }
    }