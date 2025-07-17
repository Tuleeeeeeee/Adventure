using Tuleeeeee.CoreComponets;
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

    private float _timer = 0f;
    private bool _isVisible = true;
    public override void Awake()
    {
        base.Awake();

        IdleStates = new E13_IdleStates(this, StateManager, "idle", idleStateData, this);
        MoveState = new E13_MoveState(this, StateManager, "idle", moveStateData, this);
    }
    public override void Start()
    {
        base.Start();
        StateManager.InitializeEnemy(MoveState);
    }
    public override void Update()
    {
        base.Update();
        _timer += Time.deltaTime;

        // If the ghost is visible and the timer exceeds the appear duration, disappear
        if (_isVisible && _timer >= appearDuration)
        {
            Disappear();
        }
        // If the ghost is invisible and the timer exceeds the disappear duration, appear
        else if (!_isVisible && _timer >= disappearDuration)
        {
            Appear();
        }
    }
    public void DeactiveSprite()
    {
        transform.GetComponent<SpriteRenderer>().enabled = _isVisible;
    }
    private void Appear()
    {
        Animator.SetTrigger("appear");
        Rb.gravityScale = 1;
        GetComponentInChildren<Death>().enabled = !_isVisible;
        GetComponentInChildren<Collider2D>().enabled = !_isVisible;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        _isVisible = true;                // Update visibility state
        _timer = 0f;                      // Reset the timer
    }

    private void Disappear()
    {
        Animator.SetTrigger("disappear");
        Rb.gravityScale = 0;
        GetComponentInChildren<Death>().enabled = !_isVisible;
        GetComponentInChildren<Collider2D>().enabled = !_isVisible;
        gameObject.layer = LayerMask.NameToLayer("Default");
        _isVisible = false;               // Update visibility state
        _timer = 0f;                      // Reset the timer
    }
    public override bool CheckIfCanJump() => false;
    public override bool CheckPlayerInMinAgroRange() => false;
    public override bool CheckPlayerInMaxAgroRange() => false;
    public override bool CheckPlayerInCloseRangeAction() => false;

    public override void ExecutePlayer()
    {
        Collider2D playerHit = Physics2D.OverlapBox(DamageableArea.position, entityData.damageableAreaSize, 0f, entityData.whatIsPlayer);
        if (playerHit && _isVisible)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DealDamage(100);
            }
        }
    }
}
