using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject damageableArea;
    private Animator animator;
    public float cooldownTime = 3f;
    public float fireDuration = 5f;
    public float exitDelay = 2f;

    public float checkRadius = 2f;

    public LayerMask whatIsPlayer;

    private bool isPlayerOnFire;
    private bool isCoolingDown;
    private float cooldownTimer;
    private float fireTimer;
    private bool isFireActive;


    void Start()
    {
        animator = GetComponent<Animator>();    
        damageableArea = transform.Find("DamageableArea").gameObject;
        damageableArea.SetActive(false);      // Ensure the fire is initially off
        cooldownTimer = 0f;
        fireTimer = 0f;
        isCoolingDown = false;
        isFireActive = false;
    }
    private void Update()
    {
        // Cooldown countdown
        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
                ActivateFire(); // Activate the fire when cooldown is finished
            }
        }

        // Fire active countdown
        if (isFireActive)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                DeactivateFire();  // Turn off the fire when the active duration ends
            }
        }
   
    }

    private void FixedUpdate()
    {
        // Check if the player is stepping on the fire area
        isPlayerOnFire = checkIfPlayerStepOn();
   
        if (isPlayerOnFire && !isCoolingDown && !isFireActive)
        {
            animator.SetBool("isStepOn", true);
            StartCooldown();  // Start the cooldown when player steps on it
        }
    }
    // Start cooldown when player steps on the area
    private void StartCooldown()
    {
        isCoolingDown = true;
        cooldownTimer = cooldownTime;  // Set cooldown timer
        damageableArea.SetActive(false); // Ensure the fire is off during cooldown
    }

    // Activate the fire after cooldown
    private void ActivateFire()
    {
        isCoolingDown = false;        // Stop cooling down
        isFireActive = true;          // Set fire to active
        fireTimer = fireDuration;     // Start fire active timer
        damageableArea.SetActive(true); // Turn on the fire
        animator.SetBool("isOn", isFireActive);
        animator.SetBool("isStepOn", false);
    }

    // Deactivate the fire after fire duration
    private void DeactivateFire()
    {
        isFireActive = false;          // Fire is no longer active
        damageableArea.SetActive(false); // Turn off the fire
        animator.SetBool("isOn", isFireActive);
        animator.SetBool("isOff", !isFireActive);
    }

    private bool checkIfPlayerStepOn()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
