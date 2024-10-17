using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
   // private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;
    private float bulletDamage;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;

    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;  
   

    public GameObject bulletPart1; // Reference to the first part of the split bullet
    public GameObject bulletPart2; // Reference to the second part of the split bullet

    public float splitForce = 5f;
    [SerializeField]
    private bool shotDown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        if(!shotDown)
        rb.velocity = transform.right * speed;
        else 
        rb.velocity = -transform.up * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
       /* if (!hasHitGround)
        {
            //attackDetails.position = transform.position;

          *//*  if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }*//*
        }*/
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
              
                IDamageable damageable = damageHit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(bulletDamage);
                }
                Destroy(gameObject);
            }

            if (groundHit)
            {
                hasHitGround = true;
                Destroy(gameObject);
                SplitBullet();
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }


            /*if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
              //  isGravityOn = true;
                rb.gravityScale = gravity;
            }*/
        }
    }

    public void FireProjectile(float speed, float travelDistance, float bulletDamage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.bulletDamage = bulletDamage;
    }
    void SplitBullet()
    {
        // Spawn the first part of the bullet
        GameObject part1 = Instantiate(bulletPart1, transform.position, transform.rotation);
        Rigidbody2D rb1 = part1.GetComponent<Rigidbody2D>();
        rb1.velocity = new Vector2(-1, 1).normalized * splitForce; // Adjust direction as needed
        Destroy(part1, 5f);

        // Spawn the second part of the bullet
        GameObject part2 = Instantiate(bulletPart2, transform.position, transform.rotation);
        Rigidbody2D rb2 = part2.GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(1, 1).normalized * splitForce; // Adjust direction as needed
        Destroy(part2, 5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
