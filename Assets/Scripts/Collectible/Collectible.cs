using Tuleeeeee.Enums;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public abstract class Collectible : MonoBehaviour, ICollectible
{
    [Header("Event")]
    [SerializeField] protected CollectibleEventSO collectibleEventSO;

    [Header("Collectible Settings")]
    [SerializeField] protected CollectibleType collectibleType;
    [SerializeField] protected int value = 1;

    protected Collider2D col;

    protected virtual void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }
    protected void DisableCollider()
    {
        if (col) col.enabled = false;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        OnCollected(other.gameObject);
        DisableCollider();
        RaiseCollectibleEvent();

        //Destroy(gameObject); // or pool
    }

    public abstract void OnCollected(GameObject collector);

    protected virtual void RaiseCollectibleEvent()
    {
        collectibleEventSO?.RaiseEvent(new CollectibleEventArgs(collectibleType, value));
    }
    protected virtual void AnimationFinishedTrigger()
    {
        Destroy(gameObject);
    }
}
