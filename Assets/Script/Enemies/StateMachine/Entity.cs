using System;
using UnityEngine;

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
    private Transform playerCheck;
    private Transform damageableArea;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        try
        {
            damageableArea = transform.Find("DamageableArea").gameObject.transform;
            playerCheck = transform.Find("PlayerCheck").gameObject.transform;
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
    /*
        public virtual void CheckPlayerInDamageArea()
        {
            Collider2D takeDamageArea = Physics2D.OverlapBox(damageableArea.position, entityData.damageableAreaSize, 0f, entityData.whatIsPlayer);
            if (takeDamageArea)
            {
                IDamageable damageable = takeDamageArea.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(100f);
                }
            }
        }*/
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
    public virtual void OnDead(EnemiesState deadState)
    {
        if (Stats.IsDead)
            stateManager.ChangeEnemyState(deadState);
    }
    public virtual void OnDrawGizmos()
    {
        if (!Core) return;

        if (!damageableArea) return;
        Gizmos.DrawCube(damageableArea.position, entityData.damageableAreaSize);

        if (!playerCheck) return;
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
}
