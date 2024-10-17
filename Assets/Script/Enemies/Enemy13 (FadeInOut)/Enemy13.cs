using UnityEngine;

public class Enemy13 : Entity
{
    public E13_IdleStates IdleStates { get; private set; }
    public E13_MoveState MoveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public float appearDuration = 2.0f;     // Time the ghost stays visible
    public float disappearDuration = 2.0f;  // Time the ghost stays invisible

    private float timer = 0f;
    private bool isVisible = true;
    public override void Awake()
    {
        base.Awake();

        IdleStates = new E13_IdleStates(this, stateManager, "idle", idleStateData, this);
        MoveState = new E13_MoveState(this, stateManager, "idle", moveStateData, this);
    }
    public override void Start()
    {
        base.Start();
        stateManager.InitializeEnemy(MoveState);
    }
    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        // If the ghost is visible and the timer exceeds the appear duration, disappear
        if (isVisible && timer >= appearDuration)
        {
            disappear();
        }
        // If the ghost is invisible and the timer exceeds the disappear duration, appear
        else if (!isVisible && timer >= disappearDuration)
        {
            appear();
        }
    }
    public void DeactiveSprite()
    {
        transform.GetComponent<SpriteRenderer>().enabled = isVisible;
    }
    private void appear()
    {
        Animator.SetTrigger("appear");
        RB.gravityScale = 1;
        GetComponentInChildren<Death>().enabled = !isVisible;
        GetComponentInChildren<Collider2D>().enabled = !isVisible;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        isVisible = true;                // Update visibility state
        timer = 0f;                      // Reset the timer
    }

    private void disappear()
    {
        Animator.SetTrigger("disappear");
        RB.gravityScale = 0;
        GetComponentInChildren<Death>().enabled = !isVisible;
        GetComponentInChildren<Collider2D>().enabled = !isVisible;
        gameObject.layer = LayerMask.NameToLayer("Default");
        isVisible = false;               // Update visibility state
        timer = 0f;                      // Reset the timer
    }
    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;

    public override void ExecutePlayer()
    {
        Collider2D playerHit = Physics2D.OverlapBox(damageableArea.position, entityData.damageableAreaSize, 0f, entityData.whatIsPlayer);
        if (playerHit && isVisible)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(100);
            }
        }
    }
}
