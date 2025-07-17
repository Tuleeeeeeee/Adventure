using UnityEngine;

public class FireTrap : BaseTrap
{
    [SerializeField] private GameObject damageableArea;
    [SerializeField] private float cooldownTime = 3f;
    [SerializeField] private float fireDuration = 5f;

    [SerializeField] private LayerMask whatIsPlayer;

    private Animator animator;

    private bool isPlayerOnFire;
    private bool isCoolingDown;

    private float cooldownTimer;
    private float fireTimer;
    private bool isFireActive;

    #region CHECKPLAYERSTEPON
    [SerializeField] private Vector3 offset;
    [SerializeField] private float checkRadius = 2f;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();

        damageableArea.SetActive(false);

        cooldownTimer = 0f;
        fireTimer = 0f;

        isCoolingDown = false;
        isFireActive = false;
    }
    private void Update()
    {
        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
                ActivateFire();
            }
        }

        if (isFireActive)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                DeactivateFire();
            }
        }

    }
    private void FixedUpdate()
    {
        isPlayerOnFire = CheckIfPlayerStepOn();

        if (isPlayerOnFire && !isCoolingDown && !isFireActive)
        {
            animator.SetTrigger("isStepOn");
            StartCooldown();
        }
    }
    private void StartCooldown()
    {
        isCoolingDown = true;
        cooldownTimer = cooldownTime;
        damageableArea.SetActive(false);
    }
    private void ActivateFire()
    {
        isCoolingDown = false;
        isFireActive = true;
        fireTimer = fireDuration;
        damageableArea.SetActive(true);
        animator.SetBool("isOn", isFireActive);
    }
    private void DeactivateFire()
    {
        isFireActive = false;
        damageableArea.SetActive(false);
        animator.SetBool("isOn", isFireActive);
    }
    private bool CheckIfPlayerStepOn()
    {
        return Physics2D.OverlapCircle(transform.position + offset, checkRadius, whatIsPlayer);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, checkRadius);
    }
#endif
}
