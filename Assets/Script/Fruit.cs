using System;
using UnityEditorInternal;
using UnityEngine;
using static Collectible;



public class Fruit : MonoBehaviour, ICollectible
{
    public enum CollectibleType
    {
        Fruit,
        Boost
    }

    [SerializeField]
    private int incrementValue = 0;

    private RuntimeAnimatorController collectedController;

    private Animator animator;

    public CollectibleType ItemType;

    public static event Action<int, CollectibleType> OnCollectiblesCollected;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collectedController = Resources.Load<RuntimeAnimatorController>("Items/Fruits/Collected");
    }
    public void OnCollected()
    {

        Debug.Log($"Hi You Have Collected {ItemType}");
        OnCollectiblesCollected?.Invoke(incrementValue, ItemType);
        animator.runtimeAnimatorController = collectedController;
        Destroy(gameObject, .5f);
    }
}


