using UnityEngine;

public class BaseTrap : MonoBehaviour
{
    [SerializeField] protected int damage = 999;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.DealDamage(damage);
        }
    }
}
