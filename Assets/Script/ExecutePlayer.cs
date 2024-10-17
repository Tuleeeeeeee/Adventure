using UnityEngine;

public class ExecutePlayer : MonoBehaviour
{
    [SerializeField]
    private Vector2 damageableAreaSize = Vector2.one;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private void FixedUpdate()
    {
        executePlayer();
    }
    private void executePlayer()
    {
        Collider2D playerHit = Physics2D.OverlapBox(transform.position, damageableAreaSize, 0f, whatIsPlayer);
        if (playerHit)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(100);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, damageableAreaSize);
    }
}
