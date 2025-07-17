using System;
using Tuleeeeee.Enemies;
using Tuleeeeee.Cores;
using Tuleeeeee.CoreComponets;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    protected Movement Movement
    {
        get => _movement ?? Core.GetCoreComponent(ref _movement);
    }

    protected Health Stats
    {
        get => _stats ?? Core.GetCoreComponent(ref _stats);
    }

    public Core Core { get; private set; }
    public StateManager StateManager;
    
    public D_Entity entityData;

    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public AnimationToStatemachine Atsm { get; private set; }

    private Movement _movement;
    private Health _stats;

    public Transform PlayerCheck { get; private set; }
    public Transform JumpCheck { get; private set; }
    public Transform DamageableArea { get; private set; }


    public virtual void Awake()
    {
        //this = entityData 
        Core = GetComponentInChildren<Core>();

        try
        {
            DamageableArea = transform.Find("DamageableArea")?.gameObject.transform;
            PlayerCheck = transform.Find("PlayerCheck")?.gameObject.transform;
            JumpCheck = transform.Find("JumpCheck")?.gameObject.transform;
        }
        catch (NullReferenceException)
        {
            Debug.Log($"{transform.name} does not have a PlayerCheck object, skipping assignment.");
        }
        ;


        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Atsm = GetComponent<AnimationToStatemachine>();

        StateManager = new StateManager();
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        StateManager.CurrentEnemyState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateManager.CurrentEnemyState.PhysicsUpdate();
        ExecutePlayer();
    }

    #region Check

    public virtual bool CheckIfCanJump()
    {
        return Physics2D.OverlapCircle(JumpCheck.position, 0.4f, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerCheck.position, transform.right * Movement.FacingDirection,
            entityData.minAgroDistance,
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
        RaycastHit2D hit = Physics2D.Raycast(PlayerCheck.position, transform.right * Movement.FacingDirection,
            entityData.maxAgroDistance,
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
        return Physics2D.Raycast(PlayerCheck.position, transform.right * Movement.FacingDirection,
            entityData.closeRangeActionDistance,
            entityData.whatIsPlayer);
    }

    #endregion

    public virtual void ExecutePlayer()
    {
        Collider2D playerHit = Physics2D.OverlapBox(DamageableArea.position, entityData.damageableAreaSize, 0f,
            entityData.whatIsPlayer);
        if (playerHit)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DealDamage(100);
            }
        }
    }

    public virtual void UpdateDamageableArea(Vector2 newSize, Vector3 newPosition)
    {
        // Update the size of the damageable area
        entityData.damageableAreaSize = newSize;

        // Update the local position of the damageable area
        DamageableArea.transform.localPosition = newPosition;
    }

    private void AnimtionFinishTrigger() => StateManager.CurrentEnemyState.AnimationFinishedTrigger();
    private void AnimationTrigger() => StateManager.CurrentEnemyState.AnimationTrigger();


    public virtual void OnDrawGizmos()
    {
        if (!Core) return;

        if (DamageableArea)
        {
            Gizmos.DrawCube(DamageableArea.position, entityData.damageableAreaSize);
        }

        if (PlayerCheck)
        {
            //Min
            Gizmos.color = CheckPlayerInMinAgroRange() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(
                PlayerCheck.position + (entityData.minAgroDistance * Movement.FacingDirection * transform.right), 0.6f);
            //Max
            Gizmos.color = CheckPlayerInMaxAgroRange() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(
                PlayerCheck.position + (entityData.maxAgroDistance * Movement.FacingDirection * transform.right), 0.4f);
            //
            Gizmos.color = CheckPlayerInCloseRangeAction() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(
                PlayerCheck.position +
                (entityData.closeRangeActionDistance * Movement.FacingDirection * transform.right), 0.8f);
        }

        if (JumpCheck)
        {
            Gizmos.color = CheckIfCanJump() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(JumpCheck.position, 0.8f);
        }
    }

    /*public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && ((1 << collision.gameObject.layer) & entityData.whatIsPlayer) != 0)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(100);
            }
        }
    }*/
}