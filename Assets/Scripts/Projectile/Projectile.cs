using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private bool verticalBullet;

    [SerializeField] private int bulletDamage = 100;
    [SerializeField] private float damageRadius = 0.5f;

    [Header("Split Settings")]
    [SerializeField] private GameObject bulletPart1;
    [SerializeField] private GameObject bulletPart2;
    [SerializeField] private float splitForce = 5f;

    [Header("Detection")]
    [SerializeField] private Transform damagePosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private Rigidbody2D rb;
    private bool hasHitGround;
    private float xStartPos;
    private float speed;
    private float travelDistance;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FireProjectile(float speed, float travelDistance, int bulletDamage)
    {
        this.speed = speed;
        this.bulletDamage = bulletDamage;
        this.travelDistance = travelDistance;

        rb.gravityScale = 0f;
        hasHitGround = false;

        Vector2 dir = verticalBullet ? -transform.up : transform.right;
        rb.velocity = dir * speed;

        xStartPos = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (hasHitGround) return;

        Vector2 checkPos = damagePosition.position;

        Collider2D playerHit = Physics2D.OverlapCircle(checkPos, damageRadius, whatIsPlayer);
        if (playerHit != null)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            damageable?.DealDamage(bulletDamage);
            Deactivate();
            return;
        }

        Collider2D groundHit = Physics2D.OverlapCircle(checkPos, damageRadius, whatIsGround);
        if (groundHit != null)
        {
            hasHitGround = true;
            Deactivate();
        }
        if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        ObjectPool.EnqueueObject(this, "bullet");
    }

    private void SplitBullet()
    {
        SpawnBulletPart(bulletPart1, new Vector2(-1, 1));
        SpawnBulletPart(bulletPart2, new Vector2(1, 1));
    }

    private void SpawnBulletPart(GameObject bulletPart, Vector2 direction)
    {
        if (bulletPart == null) return;

        GameObject part = Instantiate(bulletPart, transform.position, transform.rotation);
        Rigidbody2D partRb = part.GetComponent<Rigidbody2D>();
        if (partRb != null)
            partRb.velocity = direction.normalized * splitForce;

        Destroy(part, 5f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (damagePosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
#endif
}
