using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    protected Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    protected Stats Stats { get => stats ?? Core.GetCoreComponent(ref stats); }
    public Core Core { get; private set; }
    public StateManager stateManager;
    public D_Entity entityData;

    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    public AnimationToStatemachine ATSM { get; private set; }

    private Movement movement;
    private Stats stats;

    public Transform playerCheck { get; private set; }
    public Transform jumpCheck { get; private set; }
    public Transform damageableArea { get; private set; }

    [SerializeField]
    private bool flip;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        try
        {
            damageableArea = transform.Find("DamageableArea")?.gameObject.transform;
            playerCheck = transform.Find("PlayerCheck")?.gameObject.transform;
            jumpCheck = transform.Find("JumpCheck")?.gameObject.transform;
        }
        catch (NullReferenceException)
        {
            Debug.Log($"{transform.name} does not have a PlayerCheck object, skipping assignment.");
        };


        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        ATSM = GetComponent<AnimationToStatemachine>();

        stateManager = new StateManager();

    }
    public virtual void Start()
    {
        if (flip)
        {
            Movement?.Flip();
        }
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateManager.CurrentEnemyState.LogicUpdate();
        /*  if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
          {
            //  ResetStunResistance();
          }*/
    }
    public virtual void FixedUpdate()
    {
        stateManager.CurrentEnemyState.PhysicsUpdate();
        ExecutePlayer();
    }
    public virtual bool CheckIfCanJump()
    {
        return Physics2D.OverlapCircle(jumpCheck.position, 0.4f, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, transform.right * Movement.FacingDirection, entityData.minAgroDistance,
            entityData.whatIsPlayer | entityData.whatIsGround);
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & entityData.whatIsPlayer) != 0)
            {
                return true;
            }
        }
        return false;
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, transform.right * Movement.FacingDirection, entityData.maxAgroDistance,
            entityData.whatIsPlayer | entityData.whatIsGround);
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & entityData.whatIsPlayer) != 0)
            {
                return true;
            }
        }
        return false;
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right * Movement.FacingDirection, entityData.closeRangeActionDistance,
            entityData.whatIsPlayer);
    }
    public virtual void ExecutePlayer()
    {
        Collider2D playerHit = Physics2D.OverlapBox(damageableArea.position, entityData.damageableAreaSize, 0f, entityData.whatIsPlayer);
        if (playerHit)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(100);
            }
        }
    }
    public virtual void UpdateDamageableArea(Vector2 newSize, Vector3 newPosition)
    {
        // Update the size of the damageable area
        entityData.damageableAreaSize = newSize;

        // Update the local position of the damageable area
        damageableArea.transform.localPosition = newPosition;
    }
    private void AnimtionFinishTrigger() => stateManager.CurrentEnemyState.AnimationFinishedTrigger();
    private void AnimationTrigger() => stateManager.CurrentEnemyState.AnimationTrigger();
    public virtual void OnDrawGizmos()
    {
        if (!Core) return;

        if (damageableArea)
        {
            Gizmos.DrawCube(damageableArea.position, entityData.damageableAreaSize);
        }

        if (playerCheck)
        {
            //Min
            Gizmos.color = CheckPlayerInMinAgroRange() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(playerCheck.position + (entityData.minAgroDistance * Movement.FacingDirection * transform.right), 0.6f);
            //Max
            Gizmos.color = CheckPlayerInMaxAgroRange() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(playerCheck.position + (entityData.maxAgroDistance * Movement.FacingDirection * transform.right), 0.4f);
            //
            Gizmos.color = CheckPlayerInCloseRangeAction() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(playerCheck.position + (entityData.closeRangeActionDistance * Movement.FacingDirection * transform.right), 0.8f);
        }

        if (jumpCheck)
        {
            Gizmos.color = CheckIfCanJump() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(jumpCheck.position, 0.8f);
        }

    }

}
