using UnityEngine;

public class BoostCollectible : Collectible
{
    [SerializeField] private float boostForce = 12f;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void OnCollected(GameObject collector)
    {
        animator.SetTrigger("hit");

        Rigidbody2D rb = collector.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, boostForce);
        }
    }
}